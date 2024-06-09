INSERT INTO common.address ("id", "city", "country", "street", "state", "zip_code") VALUES
(1, 'Stawiszyn', 'Polska', 'Pleszewska 2', 'Wielkopolskie', '62-820'),
(2, 'Kalisz', 'Polska', 'Stawiszyńska 4', 'Wielkopolskie', '62-800'),
(3, 'Bydgoszcz', 'Polska', 'Jagielońska 9', 'Kujawsko-pomorskie', '85-097'),
(4, 'Poznań', 'Polska', 'Bułgarska 17', 'Wielkopolskie', '60-320'),
(5, 'Koźminek', 'Polska', 'Mikołaja Kopernika 15', 'Wielkopolskie', '62-840'),
(6, 'Kalisz', 'Polska', 'Górnośląska 2', 'Wielkopolskie', '62-800'),
(7, 'Warszawa', 'Polska', 'Aleje Jerozolimskie 69', 'Mazowieckie', '00-001'),
(8, 'Kraków', 'Polska', 'Floriańska 7', 'Małopolskie', '30-001'),
(9, 'Gdańsk', 'Polska', 'Długa 69', 'Pomorskie', '80-001'),
(10, 'Wrocław', 'Polska', 'Legnicka 420', 'Dolnośląskie', '51-702');
SELECT setval('common.address_id_seq', (SELECT MAX(id) FROM common.address));

INSERT INTO users.user ("id", "name", "surname", "username", "password", "created_at", "updated_at", "role", "email", "is_active", "address_id", "phone") VALUES
(1, 'Kamil', 'Owczarski', 'bilimigus', 'password', '2023-03-20 08:00:00', '2023-03-20 08:00:00', 'User', 'bilimigus@example.com', true, 1, '+48 123456789'),
(2, 'Kamil', 'Owczarzyński', 'bilililimigosu', '12345', '2023-03-21 09:00:00', '2023-03-21 09:00:00', 'Organizer', 'bilililimigosu@example.com', true, 2, '+48 696969691'),
(3, 'Jan', 'Kowalski', 'jkowalski', 'hasło123', '2022-03-21 16:00:00', '2023-1-21 09:21:14', 'User', 'jan.kowalski@example.com', false, 1, '+48 123456119'),
(4, 'Adam', 'Nowak', 'anowak', 'haslo123', '2022-01-05 10:30:00', '2022-02-15 14:20:45', 'Organizer', 'adam.nowak@example.com', false, 2, '+48 987654321'),
(5, 'Ewa', 'Kowalska', 'ekowalska', 'tajnehaslo', '2022-02-10 08:15:00', '2022-03-25 12:45:30', 'User', 'ewa.kowalska@example.com', false, 3, '+48 654321987'),
(6, 'Piotr', 'Wiśniewski', 'pwiśniewski', 'password123', '2022-03-15 11:45:00', '2022-04-30 16:10:20', 'Admin', 'piotr.wisniewski@example.com', false, 4, '+48 321654987'),
(7, 'Anna', 'Lis', 'alis', '123456', '2022-04-20 13:20:00', '2022-05-10 09:55:15', 'Organizer', 'anna.lis@example.com', true, 5, '+48 789456123'),
(8, 'Tomasz', 'Zieliński', 'tzielinski', 'qwerty', '2022-05-25 15:00:00', '2022-06-20 11:30:10', 'User', 'tomasz.zielinski@example.com', true, 6, '+48 456123789'),
(9, 'Magdalena', 'Kowalczyk', 'mkowalczyk', 'mojehaslo', '2022-06-30 17:10:00', '2022-07-15 13:15:25', 'Admin', 'magdalena.kowalczyk@example.com', true, 7, '+48 987654123'),
(10, 'Marcin', 'Szymański', 'mszymanski', 'password', '2022-07-05 09:00:00', '2022-08-05 08:20:35', 'Organizer', 'marcin.szymanski@example.com', true, 8, '+48 321789654');
SELECT setval('users.user_id_seq', (SELECT MAX(id) FROM users.user));

INSERT INTO events.event ("id", "name", "address_id", "description", "organizer_id", "start_at", "end_at", "created_at", "updated_at", "slug", "status") VALUES
(1, 'Tech Conference', 1, 'Annual tech conference', 1, '2023-10-01 09:00:00', '2023-10-02 17:00:00', '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'tech-conference', 'Approved'),
(2, 'Health Symposium', 2, 'Health and wellness symposium', 2, '2023-11-05 09:00:00', '2023-11-06 17:00:00', '2023-10-05 08:00:00', '2023-10-05 08:00:00', 'health-symposium', 'InProgress'),
(3, 'Koncert Rockowy', 3, 'Występ ulubionych zespołów rockowych', 2, CURRENT_TIMESTAMP - '1 day'::INTERVAL, CURRENT_TIMESTAMP + '4 hours'::INTERVAL, '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'koncert-rockowy', 'WaitingForApproval'),
(4, 'Konferencja IT', 4, 'Coroczna konferencja technologiczna', 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + '3 hours'::INTERVAL, '2023-10-02 17:00:00', '2023-10-02 18:00:00', 'konferencja-it', 'Completed'),
(5, 'Mecz Piłki Nożnej', 5, 'Mecz drużynowej rywalizacji w piłce nożnej', 3, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + '3 hours'::INTERVAL, '2022-12-01 08:00:00', '2023-09-01 08:30:00', 'mecz-pilki-noznej', 'InProgress'),
(6, 'Festiwal Elektroniczny', 6, 'Największe hity muzyki elektronicznej', 4, CURRENT_TIMESTAMP - '4 day'::INTERVAL, CURRENT_TIMESTAMP - '2 days'::INTERVAL, '2023-09-01 08:00:00', '2023-09-02 08:00:00', 'festiwal-elektroniczny', 'WaitingForApproval'),
(7, 'Koncert Hip-Hopowy', 7, 'Najnowsze hity hip-hopu w wykonaniu gwiazd', 6, CURRENT_TIMESTAMP + '2 days'::INTERVAL, CURRENT_TIMESTAMP + '2 days'::INTERVAL + '4 hours'::INTERVAL, '2023-10-08 17:00:00', '2023-11-01 09:19:22', 'koncert-hip-hopowy', 'Completed'),
(8, 'Wieczór Jazzowy', 8, 'Relaksujące dźwięki jazzu w kameralnej atmosferze', 8, CURRENT_TIMESTAMP + '1 day'::INTERVAL, CURRENT_TIMESTAMP + '1 day'::INTERVAL + '5 hours'::INTERVAL, '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'wieczor-jazzowy', 'InProgress'),
(9, 'Koncert Klasyczny', 9, 'Muzyka klasyczna w wykonaniu renomowanych artystów', 10, CURRENT_TIMESTAMP + '1 day'::INTERVAL, CURRENT_TIMESTAMP + '1 day'::INTERVAL + '3 hours'::INTERVAL, '2023-01-01 15:00:00', '2023-02-01 08:00:00', 'koncert-klasyczny', 'WaitingForApproval'),
(10, 'Turniej w League of Legends', 10, 'Turniej LAN dla miłośnikow esportu i gry League of Legends', 10, CURRENT_TIMESTAMP - '1 day'::INTERVAL, CURRENT_TIMESTAMP - '3 hours'::INTERVAL, '2023-01-01 15:00:00', '2023-02-01 08:00:00', 'turniej-lol', 'Completed');
SELECT setval('events.event_id_seq', (SELECT MAX(id) FROM events.event));

INSERT INTO events.tag ("id", "name") VALUES
(1, 'Technologia'),
(2, 'Sport'),
(3, 'Piłka nożna'),
(4, 'Muzyka'),
(5, 'Rock'),
(6, 'Koszykówka'),
(7, 'Hip Hop'),
(8, 'Esport'),
(9, 'League of Legends'),
(10, 'Zdrowie');
SELECT setval('events.tag_id_seq', (SELECT MAX(id) FROM events.tag));

INSERT INTO events.event_tag ("event_id", "tag_id") VALUES
(1, 1),
(2, 10),
(3, 5),
(3, 4),
(4, 1),
(5, 2),
(5, 3),
(6, 4),
(7, 7),
(8, 4),
(9, 4),
(10, 8),
(10, 9);

INSERT INTO events.participant ("id", "event_id", "user_id", "status") VALUES
(1, 1, 1, 'Interested'),
(2, 2, 2, 'Participating'),
(3, 3, 3, 'Interested'),
(4, 4, 4, 'Participating'),
(5, 5, 5, 'Interested'),
(6, 6, 6, 'Participating'),
(7, 7, 7, 'Interested'),
(8, 7, 8, 'Participating'),
(9, 9, 9, 'Interested'),
(10, 10, 10, 'Participating');
SELECT setval('events.participant_id_seq', (SELECT MAX(id) FROM events.participant));

INSERT INTO events.parameter ("id", "key", "value") VALUES
(1, 'Liczba Uczestników', '1000'),
(2, 'Długość Trasy', '10 km'),
(3, 'Temperatura', '25 C'),
(4, 'Czas Trwania', '2 godziny'),
(5, 'Poziom Trudności', 'Średni'),
(6, 'Rozdawane Nagrody', '500 USD'),
(7, 'Typ Wydarzenia', 'Bezpośrednie'),
(8, 'Czas Trwania', '3 dni'),
(9, 'Czas Trwania', '10 dni'),
(10, 'Miejsce Wydarzenia', 'Plenerowe');
SELECT setval('events.parameter_id_seq', (SELECT MAX(id) FROM events.parameter)); 
 
INSERT INTO events.event_parameter ("parameter_id", "event_id") VALUES
(1, 1),
(2, 2),
(3, 3),
(4, 4),
(5, 5),
(6, 6),
(7, 7),
(8, 8),
(9, 9),
(10, 10);

INSERT INTO events.comment ("id", "event_id", "content", "created_at", "user_id") VALUES
(1, 1, 'Nie mogę się doczekać!', '2023-09-10', 1),
(2, 2, 'Trochę nudne...', '2023-10-15', 2),
(3, 3, 'Super wydarzenie!', '2023-11-20', 3),
(4, 4, 'Fantastyczne doświadczenie!', '2023-12-05', 4),
(5, 5, 'Zapowiada się świetnie!', '2024-01-02', 5),
(6, 6, 'Jestem podekscytowany!', '2024-02-14', 6),
(7, 7, 'Bardzo ciekawe!', '2024-03-21', 7),
(8, 8, 'Jestem zachwycony!', '2024-04-30', 8),
(9, 9, 'To będzie świetna zabawa!', '2024-05-18', 9),
(10, 10, 'Oki', '2024-06-25', 10);
SELECT setval('events.comment_id_seq', (SELECT MAX(id) FROM events.comment));
 
INSERT INTO users.interest ("id", "name") VALUES
(1, 'Programowanie'),
(2, 'Sport'),
(3, 'Muzyka'),
(4, 'Film'),
(5, 'Taniec'),
(6, 'Fitness'),
(7, 'Sztuka'),
(8, 'Kulinaria'),
(9, 'Podróże'),
(10, 'Gry komputerowe');
SELECT setval('users.interest_id_seq', (SELECT MAX(id) FROM users.interest)); 
 
INSERT INTO users.user_interest ("user_id", "interest_id", "level") VALUES
(1, 1, 3),
(2, 2, 2),
(3, 3, 5),
(4, 4, 2),
(5, 5, 3),
(6, 6, 4),
(7, 7, 6),
(8, 8, 3),
(9, 9, 1),
(10, 10, 5);