-- Para consegui gerar o uuid
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE public.pessoas (
	id UUID DEFAULT uuid_generate_v4 () PRIMARY KEY NOT NULL,
	apelido VARCHAR(32) UNIQUE NOT NULL,
	nome VARCHAR(100) NOT NULL,
	nascimento VARCHAR(10) NOT NULL,
	stack TEXT NULL,
	search_text TEXT NULL
);

CREATE INDEX idx_search_text ON public.pessoas (search_text);

-- Crie a função que será chamada pelo gatilho
CREATE OR REPLACE FUNCTION atualizar_search_text()
RETURNS TRIGGER AS $$
BEGIN
  NEW.search_text := CONCAT_WS(', ', NEW.apelido, NEW.nome, NEW.stack);
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

-- Crie o gatilho que chama a função antes de uma inserção ou atualização
CREATE TRIGGER trigger_atualizar_search_text
BEFORE INSERT OR UPDATE
ON public.pessoas
FOR EACH ROW
EXECUTE FUNCTION atualizar_search_text();