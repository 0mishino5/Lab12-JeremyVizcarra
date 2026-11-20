CREATE TABLE IF NOT EXISTS notifications (
    id SERIAL PRIMARY KEY,
    username VARCHAR(100) NOT NULL,
    message VARCHAR(255) NOT NULL,
    type VARCHAR(30) NOT NULL,
    sent_at TIMESTAMP NOT NULL DEFAULT NOW()
);

INSERT INTO notifications (username, message, type, sent_at)
SELECT username, message, type, sent_at
FROM (VALUES
    ('usuario_antiguo1', 'Notificacion de prueba antigua', 'Inicial', NOW() - INTERVAL '60 days'),
    ('usuario_antiguo2', 'Notificacion de prueba antigua', 'Inicial', NOW() - INTERVAL '45 days'),
    ('usuario_reciente', 'Notificacion de prueba reciente', 'Inicial', NOW() - INTERVAL '2 days')
) AS datos (username, message, type, sent_at)
WHERE NOT EXISTS (SELECT 1 FROM notifications);
