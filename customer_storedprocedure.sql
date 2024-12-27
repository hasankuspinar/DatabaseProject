CREATE DEFINER=`root`@`localhost` PROCEDURE `AddNewCustomer`(
    IN p_name VARCHAR(100),
    IN p_phone VARCHAR(20),
    IN p_company_id INT,
    IN p_address VARCHAR(100)
)
BEGIN
    INSERT INTO Customer (name, phone, company_id, address)
    VALUES (p_name, p_phone, p_company_id, p_address);
END