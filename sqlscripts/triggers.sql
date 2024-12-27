CREATE TABLE CargoCompany (
    company_id INT PRIMARY KEY AUTO_INCREMENT,
    company_name VARCHAR(100),
    rating DECIMAL(10, 2),
    address VARCHAR(255)
);

CREATE TABLE Customer (
    customer_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    address VARCHAR(255),
    phone VARCHAR(20),
    company_id INT NOT NULL,
    FOREIGN KEY (company_id) REFERENCES CargoCompany(company_id) ON DELETE CASCADE ON UPDATE CASCADE
);


CREATE TABLE Route (
    route_id INT PRIMARY KEY AUTO_INCREMENT,
    source_address VARCHAR(255) NOT NULL,
    destination_address VARCHAR(255) NOT NULL
);

CREATE TABLE Courier (
    courier_id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(100),
    phone VARCHAR(20)
);

CREATE TABLE TransportVehicle (
    vehicle_id INT PRIMARY KEY AUTO_INCREMENT,
    type VARCHAR(50),
    model VARCHAR(50),
    courier_id INT NOT NULL,
    FOREIGN KEY (courier_id) REFERENCES Courier(courier_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Shipment (
    shipment_id INT PRIMARY KEY AUTO_INCREMENT,
    transport_vehicle_id INT NOT NULL,
    route_id INT NOT NULL,
    company_id INT NOT NULL,
    FOREIGN KEY (transport_vehicle_id) REFERENCES TransportVehicle(vehicle_id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (route_id) REFERENCES Route(route_id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (company_id) REFERENCES CargoCompany(company_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Cargo (
    cargo_id INT PRIMARY KEY AUTO_INCREMENT,
    shipment_id INT NOT NULL,
    customer_id INT NOT NULL,
    weight DECIMAL(10, 2),
    volume DECIMAL(10, 2),
    type VARCHAR(50),
    FOREIGN KEY (shipment_id) REFERENCES Shipment(shipment_id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (customer_id) REFERENCES Customer(customer_id) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Tracking (
    tracking_id INT PRIMARY KEY AUTO_INCREMENT,
    shipment_id INT NOT NULL,
    status VARCHAR(50),
    FOREIGN KEY (shipment_id) REFERENCES Shipment(shipment_id) ON DELETE CASCADE ON UPDATE CASCADE
);

DELIMITER $$

CREATE TRIGGER before_cargo_company_insert
BEFORE INSERT ON CargoCompany
FOR EACH ROW
BEGIN
    IF NEW.rating IS NULL THEN
        SET NEW.rating = 3.5;
    END IF;
END$$

DELIMITER ;

DELIMITER $$

CREATE TRIGGER before_shipment_delete
BEFORE DELETE ON Shipment
FOR EACH ROW
BEGIN
    DECLARE shipment_status VARCHAR(50);
    SELECT status INTO shipment_status FROM Tracking WHERE shipment_id = OLD.shipment_id;
    IF shipment_status = 'In Transit' THEN
        SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = 'Cannot delete shipment that is in transit';
    END IF;
END$$

DELIMITER ;

DELIMITER $$

CREATE TRIGGER trg_prevent_duplicate_shipment
BEFORE INSERT ON Shipment
FOR EACH ROW
BEGIN
    IF EXISTS (
        SELECT 1
        FROM Shipment
        WHERE transport_vehicle_id = NEW.transport_vehicle_id
        AND route_id = NEW.route_id
    ) THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'Duplicate shipment with the same vehicle and route is not allowed';
    END IF;
END;
DELIMITER;
