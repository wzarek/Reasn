INSERT INTO common.address ("id", "city", "country", "street", "state", "zip_code") VALUES
(1, 'Stawiszyn', 'Polska', 'Pleszewska 2', 'Wielkopolskie', '62-820'),
(2, 'Kalisz', 'Polska', 'Stawiszyńska 4', 'Wielkopolskie', '62-800'),
(3, 'Bydgoszcz', 'Polska', 'Jagielońska 12', 'Kujawsko-Pomorskie', '85-097'),
(4, 'Poznań', 'Polska', 'Bułgarska 17', 'Wielkopolskie', '60-320'),
(5, 'Koźminek', 'Polska', 'Mikołaja Kopernika', 'Wielkopolskie', '62-840'),
(6, 'Kalisz', 'Polska', 'Górnośląska 2', 'Wielkopolskie', '62-800'),
(7, 'Warszawa', 'Polska', 'Aleje Jerozolimskie', 'Mazowieckie', '00-001'),
(8, 'Kraków', 'Polska', 'ul. Floriańska 7', 'Małopolskie', '30-001'),
(9, 'Gdańsk', 'Polska', 'ul. Długa 69', 'Pomorskie', '80-001'),
(10, 'Wrocław', 'Polska', 'Legnicka 420', 'Dolnośląskie', '51-702');

INSERT INTO users.user ("id", "name", "surname", "username", "password", "created_at", "updated_at", "role_id", "email", "is_active", "address_id", "phone") VALUES
(1, 'Kamil', 'Owczarski', 'bilimigus', 'password', '2023-03-20 08:00:00', '2023-03-20 08:00:00', 1, 'bilimigus@example.com', true, 1, '123456789'),
(2, 'Kamil', 'Owczarzyński', 'bilililimigosu', '12345', '2023-03-21 09:00:00', '2023-03-21 09:00:00', 2, 'bilililimigosu@example.com', true, 2, '696969691'),
(3, 'Jan', 'Kowalski', 'jkowalski', 'hasło123', '2022-03-21 16:00:00', '2023-1-21 09:21:14', 1, 'jan.kowalski@example.com', false, 1, '123456789'),
(4, 'Adam', 'Nowak', 'anowak', 'haslo123', '2022-01-05 10:30:00', '2022-02-15 14:20:45', 2, 'adam.nowak@example.com', false, 2, '987654321'),
(5, 'Ewa', 'Kowalska', 'ekowalska', 'tajnehaslo', '2022-02-10 08:15:00', '2022-03-25 12:45:30', 1, 'ewa.kowalska@example.com', false, 3, '654321987'),
(6, 'Piotr', 'Wiśniewski', 'pwiśniewski', 'password123', '2022-03-15 11:45:00', '2022-04-30 16:10:20', 3, 'piotr.wisniewski@example.com', false, 4, '321654987'),
(7, 'Anna', 'Lis', 'alis', '123456', '2022-04-20 13:20:00', '2022-05-10 09:55:15', 2, 'anna.lis@example.com', true, 5, '789456123'),
(8, 'Tomasz', 'Zieliński', 'tzielinski', 'qwerty', '2022-05-25 15:00:00', '2022-06-20 11:30:10', 1, 'tomasz.zielinski@example.com', true, 6, '456123789'),
(9, 'Magdalena', 'Kowalczyk', 'mkowalczyk', 'mojehaslo', '2022-06-30 17:10:00', '2022-07-15 13:15:25', 3, 'magdalena.kowalczyk@example.com', true, 7, '987654123'),
(10, 'Marcin', 'Szymański', 'mszymanski', 'password', '2022-07-05 09:00:00', '2022-08-05 08:20:35', 2, 'marcin.szymanski@example.com', true, 8, '321789654');
  

INSERT INTO events.event ("id", "name", "address_id", "description", "organizer_id", "start_at", "end_at", "created_at", "updated_at", "slug", "status_id") VALUES
(1, 'Tech Conference', 1, 'Annual tech conference', 1, '2023-10-01 09:00:00', '2023-10-02 17:00:00', '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'tech-conference', 1),
(2, 'Health Symposium', 2, 'Health and wellness symposium', 2, '2023-11-05 09:00:00', '2023-11-06 17:00:00', '2023-10-05 08:00:00', '2023-10-05 08:00:00', 'health-symposium', 2),
(3, 'Koncert Rockowy', 3, 'Występ ulubionych zespołów rockowych', 2, CURRENT_TIMESTAMP - '1 day'::INTERVAL, CURRENT_TIMESTAMP + '4 hours'::INTERVAL, '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'koncert-rockowy', 1),
(4, 'Konferencja IT', 4, 'Coroczna konferencja technologiczna', 1, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + '3 hours'::INTERVAL, '2023-10-02 17:00:00', '2023-10-02 18:00:00', 'konferencja-it', 1),
(5, 'Mecz Piłki Nożnej', 5, 'Mecz drużynowej rywalizacji w piłce nożnej', 3, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP + '3 hours'::INTERVAL, '2022-12-01 08:00:00', '2023-09-01 08:30:00', 'mecz-pilki-noznej', 1),
(6, 'Festiwal Elektroniczny', 6, 'Największe hity muzyki elektronicznej', 4, CURRENT_TIMESTAMP - '4 day'::INTERVAL, CURRENT_TIMESTAMP - '2 days'::INTERVAL, '2023-09-01 08:00:00', '2023-09-02 08:00:00', 'festiwal-elektroniczny', 1),
(7, 'Koncert Hip-Hopowy', 7, 'Najnowsze hity hip-hopu w wykonaniu gwiazd', 6, CURRENT_TIMESTAMP + '2 days'::INTERVAL, CURRENT_TIMESTAMP + '2 days'::INTERVAL + '4 hours'::INTERVAL, '2023-10-08 17:00:00', '2023-11-01 09:19:22', 'koncert-hip-hopowy', 1),
(8, 'Wieczór Jazzowy', 8, 'Relaksujące dźwięki jazzu w kameralnej atmosferze', 8, CURRENT_TIMESTAMP + '1 day'::INTERVAL, CURRENT_TIMESTAMP + '1 day'::INTERVAL + '5 hours'::INTERVAL, '2023-09-01 08:00:00', '2023-09-01 08:00:00', 'wieczor-jazzowy', 1),
(9, 'Koncert Klasyczny', 9, 'Muzyka klasyczna w wykonaniu renomowanych artystów', 10, CURRENT_TIMESTAMP + '1 day'::INTERVAL, CURRENT_TIMESTAMP + '1 day'::INTERVAL + '3 hours'::INTERVAL, '2023-01-01 15:00:00', '2023-02-01 08:00:00', 'koncert-klasyczny', 1),
(10, 'Turniej w League of Legends', 10, 'Turniej LAN dla miłośnikow esportu i gry League of Legends', 10, CURRENT_TIMESTAMP - '1 day'::INTERVAL, CURRENT_TIMESTAMP - '3 hours'::INTERVAL, '2023-01-01 15:00:00', '2023-02-01 08:00:00', 'turniej-lol', 1);

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

INSERT INTO events.participant ("id", "event_id", "user_id", "status_id") VALUES
(1, 1, 1, 2),
(2, 2, 2, 2),
(3, 3, 3, 2),
(4, 4, 4, 2),
(5, 5, 5, 2),
(6, 6, 6, 2),
(7, 7, 7, 2),
(8, 7, 8, 2),
(9, 9, 9, 2),
(10, 10, 10, 2);

INSERT INTO events.parameter ("id", "key", "value") VALUES
(1, 'Liczba Uczestników', '1000'),
(2, 'Długość Trasy', '10 km'),
(3, 'Temperatura', '25°C'),
(4, 'Czas Trwania', '2 godziny'),
(5, 'Poziom Trudności', 'Średni'),
(6, 'Rozdawane Nagrody', '500 USD'),
(7, 'Typ Wydarzenia', 'Bezpośrednie'),
(8, 'Czas Trwania', '3 dni'),
(9, 'Czas Trwania', '10 dni'),
(10, 'Miejsce Wydarzenia', 'Plenerowe');
 
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
(10, 10, 'jd orka', '2024-06-25', 10);
 
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
 
INSERT INTO users.user_interest ("user_id", "interest_id", "level") VALUES
(1, 1, 3),
(2, 2, 2),
(3, 3, 5),
(4, 4, 2),
(5, 5, 3),
(6, 6, 4),
(7, 7, 6),
(8, 8,3),
(9, 9, 1),
(10, 10, 5);
