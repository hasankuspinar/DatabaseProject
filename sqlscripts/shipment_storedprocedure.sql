CREATE DEFINER=`root`@`localhost` PROCEDURE `DeleteShipment`(
    IN p_shipment_id INT
)
BEGIN
    DELETE FROM Cargo WHERE shipment_id = p_shipment_id;
    DELETE FROM Tracking WHERE shipment_id = p_shipment_id;
    DELETE FROM Shipment WHERE shipment_id = p_shipment_id;
END
