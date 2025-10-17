-- Создаём таблицу raw_events для хранения всех входящих событий
CREATE TABLE IF NOT EXISTS raw_events (
  id            BIGSERIAL PRIMARY KEY,       -- уникальный идентификатор события
  tenant_id     TEXT NOT NULL,               -- идентификатор клиентапроекта
  user_id       TEXT,                        -- идентификатор пользователя (опционально)
  session_id    TEXT,                        -- идентификатор сессии (опционально)
  type          TEXT NOT NULL,               -- тип события (например page_view, click)
  ts_utc        TIMESTAMPTZ NOT NULL,        -- время события в UTC
  properties    JSONB NOT NULL DEFAULT '{}'jsonb -- дополнительные свойства в формате JSON
);

-- Индекс для ускорения поиска по клиенту и времени (часто будем использовать для дашбордов)
CREATE INDEX IF NOT EXISTS ix_raw_events_tenant_ts
  ON raw_events (tenant_id, ts_utc DESC);

-- Индекс для ускорения поиска по типу события и времени
CREATE INDEX IF NOT EXISTS ix_raw_events_type_ts
  ON raw_events (type, ts_utc DESC);

-- GIN-индекс по JSONB свойствам (ускоряет запросы по вложенным ключам)
CREATE INDEX IF NOT EXISTS ix_raw_events_gin_props
  ON raw_events USING GIN (properties);
