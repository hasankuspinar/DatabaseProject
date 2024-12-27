CREATE DEFINER=`root`@`localhost` PROCEDURE `GetCustomerShipments`(
    IN p_customer_id INT
)
BEGIN
    SELECT DISTINCT
        s.shipment_id,
        tv.type AS transport_vehicle_type,
        r.source_address AS route_source,
        r.destination_address AS route_destination,
        cc.company_name AS company_name
    FROM Shipment s
    INNER JOIN Cargo c ON s.shipment_id = c.shipment_id
    INNER JOIN TransportVehicle tv ON s.transport_vehicle_id = tv.vehicle_id
    INNER JOIN Route r ON s.route_id = r.route_id
    INNER JOIN CargoCompany cc ON s.company_id = cc.company_id
    WHERE c.customer_id = p_customer_id;
END