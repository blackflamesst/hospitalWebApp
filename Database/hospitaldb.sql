-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Хост: MySQL-8.2
-- Время создания: Окт 15 2024 г., 02:56
-- Версия сервера: 8.2.0
-- Версия PHP: 8.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- База данных: `Hospital`
--

-- --------------------------------------------------------

--
-- Структура таблицы `appointments`
--

CREATE TABLE `appointments` (
  `id` int UNSIGNED NOT NULL,
  `doctor_id` int UNSIGNED NOT NULL,
  `procedure_id` int UNSIGNED NOT NULL,
  `customer_id` int UNSIGNED NOT NULL,
  `appointment_date` date NOT NULL,
  `appointment_time` time NOT NULL,
  `total_cost` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Записи к врачу';

--
-- Дамп данных таблицы `appointments`
--

INSERT INTO `appointments` (`id`, `doctor_id`, `procedure_id`, `customer_id`, `appointment_date`, `appointment_time`, `total_cost`) VALUES
(1, 1, 1, 5, '2024-10-17', '14:00:00', 2500),
(2, 5, 3, 3, '2024-10-12', '07:30:00', 600),
(3, 2, 2, 4, '2024-10-23', '09:00:00', 1800),
(4, 3, 1, 2, '2024-10-27', '13:00:00', 2500),
(5, 5, 5, 1, '2024-10-20', '15:30:00', 7600),
(6, 5, 4, 4, '2024-10-20', '13:30:00', 7500),
(7, 4, 2, 3, '2024-10-22', '11:00:00', 1800);

-- --------------------------------------------------------

--
-- Структура таблицы `categories`
--

CREATE TABLE `categories` (
  `id` int UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Категории направлений лечения';

--
-- Дамп данных таблицы `categories`
--

INSERT INTO `categories` (`id`, `name`) VALUES
(1, 'Терапевт'),
(2, 'Офтальмолог'),
(3, 'Отоларинголог'),
(4, 'Хирург'),
(5, 'Лаборант');

-- --------------------------------------------------------

--
-- Структура таблицы `customers`
--

CREATE TABLE `customers` (
  `id` int UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL,
  `age` int NOT NULL,
  `date_of_birth` date NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `customers`
--

INSERT INTO `customers` (`id`, `name`, `age`, `date_of_birth`) VALUES
(1, 'Баринова Виктория Алексеевна', 19, '2005-07-31'),
(2, 'Калинин Макар Александрович', 26, '1997-12-10'),
(3, 'Волков Матвей Павлович', 37, '1987-04-06'),
(4, 'Филимонова Екатерина Владиславовна', 32, '1992-03-16'),
(5, 'Филатова Алиса Давидовна', 21, '2003-01-01');

-- --------------------------------------------------------

--
-- Структура таблицы `doctors`
--

CREATE TABLE `doctors` (
  `id` int UNSIGNED NOT NULL,
  `name` varchar(50) NOT NULL,
  `category_id` int UNSIGNED NOT NULL,
  `office` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `doctors`
--

INSERT INTO `doctors` (`id`, `name`, `category_id`, `office`) VALUES
(1, 'Ковалёв Евгений Олегович', 1, 101),
(2, 'Харитонов Владимир Константинович', 2, 102),
(3, 'Шагоходов Валерий Анатольевич', 3, 103),
(4, 'Безруков Алексей Андреевич', 4, 104),
(5, 'Кровососова Наталья Александровна', 5, 110);

-- --------------------------------------------------------

--
-- Структура таблицы `doctor_procedures`
--

CREATE TABLE `doctor_procedures` (
  `doctor_id` int UNSIGNED NOT NULL,
  `procedure_id` int UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Дамп данных таблицы `doctor_procedures`
--

INSERT INTO `doctor_procedures` (`doctor_id`, `procedure_id`) VALUES
(1, 1),
(2, 1),
(3, 1),
(4, 1),
(1, 2),
(2, 2),
(3, 2),
(4, 2),
(5, 3),
(5, 4),
(5, 5);

-- --------------------------------------------------------

--
-- Структура таблицы `procedures`
--

CREATE TABLE `procedures` (
  `id` int UNSIGNED NOT NULL,
  `type` varchar(50) NOT NULL,
  `price` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Процедуры: Вид и цена.';

--
-- Дамп данных таблицы `procedures`
--

INSERT INTO `procedures` (`id`, `type`, `price`) VALUES
(1, 'Первичный приём', 2500),
(2, 'Вторичный приём', 1800),
(3, 'Общий анализ крови', 600),
(4, 'МРТ Шейного отдела позвоночника', 7500),
(5, 'МРТ спинного отдела позвоночника', 7600);

--
-- Индексы сохранённых таблиц
--

--
-- Индексы таблицы `appointments`
--
ALTER TABLE `appointments`
  ADD PRIMARY KEY (`id`),
  ADD KEY `doctor_id` (`doctor_id`),
  ADD KEY `procedure_id` (`procedure_id`),
  ADD KEY `customer_id` (`customer_id`);

--
-- Индексы таблицы `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`id`);

--
-- Индексы таблицы `doctors`
--
ALTER TABLE `doctors`
  ADD PRIMARY KEY (`id`),
  ADD KEY `category_id` (`category_id`);

--
-- Индексы таблицы `doctor_procedures`
--
ALTER TABLE `doctor_procedures`
  ADD PRIMARY KEY (`doctor_id`,`procedure_id`),
  ADD KEY `procedure_id` (`procedure_id`);

--
-- Индексы таблицы `procedures`
--
ALTER TABLE `procedures`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT для сохранённых таблиц
--

--
-- AUTO_INCREMENT для таблицы `appointments`
--
ALTER TABLE `appointments`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT для таблицы `categories`
--
ALTER TABLE `categories`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `customers`
--
ALTER TABLE `customers`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `doctors`
--
ALTER TABLE `doctors`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT для таблицы `procedures`
--
ALTER TABLE `procedures`
  MODIFY `id` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- Ограничения внешнего ключа сохраненных таблиц
--

--
-- Ограничения внешнего ключа таблицы `appointments`
--
ALTER TABLE `appointments`
  ADD CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`doctor_id`) REFERENCES `doctors` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `appointments_ibfk_2` FOREIGN KEY (`procedure_id`) REFERENCES `procedures` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `appointments_ibfk_3` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `doctors`
--
ALTER TABLE `doctors`
  ADD CONSTRAINT `doctors_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;

--
-- Ограничения внешнего ключа таблицы `doctor_procedures`
--
ALTER TABLE `doctor_procedures`
  ADD CONSTRAINT `doctor_procedures_ibfk_1` FOREIGN KEY (`doctor_id`) REFERENCES `doctors` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `doctor_procedures_ibfk_2` FOREIGN KEY (`procedure_id`) REFERENCES `procedures` (`id`) ON DELETE RESTRICT ON UPDATE RESTRICT;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
