-- noinspection SqlNoDataSourceInspectionForFile

CREATE TABLE IF NOT EXISTS category
(
    id         UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    name       TEXT NOT NULL,
    deleted_at TIMESTAMP
);

CREATE TABLE IF NOT EXISTS series
(
    id           UUID PRIMARY KEY DEFAULT gen_random_uuid(),
    title        TEXT NOT NULL,
    description  TEXT,
    image_url    TEXT,
    release_date DATE,
    added_at     TIMESTAMP        DEFAULT now(),
    deleted_at   TIMESTAMP
);

CREATE TABLE IF NOT EXISTS series_category
(
    series_id   UUID REFERENCES series (id)   NOT NULL,
    category_id UUID REFERENCES category (id) NOT NULL,
    PRIMARY KEY (series_id, category_id)
);
