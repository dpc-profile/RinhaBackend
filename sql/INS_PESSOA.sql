CREATE OR REPLACE FUNCTION INS_PESSOA(
    pApelido VARCHAR(32),
    pNome VARCHAR(100),
    pNascimento VARCHAR(10),
    pStack TEXT
)
RETURNS UUID AS $$
DECLARE
    novo_id UUID;
BEGIN
    INSERT INTO pessoas(apelido, nome, nascimento, stack)
    VALUES(pApelido, pNome, pNascimento, pStack)
    ON CONFLICT (apelido) DO NOTHING
    RETURNING id INTO novo_id;
    
    RETURN novo_id;
    
END;
$$ LANGUAGE plpgsql;
