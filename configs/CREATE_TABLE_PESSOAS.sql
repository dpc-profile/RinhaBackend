CREATE TABLE public.pessoas (
	id UUID PRIMARY KEY NOT NULL,
	apelido VARCHAR(32) UNIQUE NOT NULL,
	nome VARCHAR(100) NOT NULL,
	nascimento DATE NOT NULL,
	stack TEXT NULL
);

CREATE INDEX idx_stack ON public.pessoas (stack);