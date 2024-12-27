using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Google.Cloud.Firestore;

namespace CS306_Phase2.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    [FirestoreData]
    public class User
    {
        [FirestoreProperty("username")]
        public string Username { get; set; }

        [FirestoreProperty("password")]
        public string Password { get; set; }

        [FirestoreProperty("userRole")]
        public string UserRole { get; set; }
    }
    public class CargoCompany
    {
        [Key]
        [Column("company_id")]
        public int CompanyId { get; set; }

        [Column("company_name")]
        public string CompanyName { get; set; }

        [Column("rating")]
        public decimal Rating { get; set; }

        [Column("address")]
        public string Address { get; set; }
    }


    public class Customer
    {
        [Key]
        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }
    }


    public class Route
    {
        [Key]
        [Column("route_id")]
        public int RouteId { get; set; }

        [Column("source_address")]
        public string SourceAddress { get; set; }

        [Column("destination_address")]
        public string DestinationAddress { get; set; }
    }


    public class Courier
    {
        [Key]
        [Column("courier_id")]
        public int CourierId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("phone")]
        public string Phone { get; set; }
    }


    public class TransportVehicle
    {
        [Key]
        [Column("vehicle_id")]
        public int VehicleId { get; set; }

        [Column("type")]
        public string Type { get; set; }

        [Column("model")]
        public string Model { get; set; }

        [Column("courier_id")]
        public int CourierId { get; set; }
    }


    public class Shipment
    {
        [Key]
        [Column("shipment_id")]
        public int ShipmentId { get; set; }

        [Column("transport_vehicle_id")]
        public int TransportVehicleId { get; set; }

        [Column("route_id")]
        public int RouteId { get; set; }

        [Column("company_id")]
        public int CompanyId { get; set; }
    }


    public class Cargo
    {
        [Key]
        [Column("cargo_id")]
        public int CargoId { get; set; }

        [Column("shipment_id")]
        public int ShipmentId { get; set; }

        [Column("customer_id")]
        public int CustomerId { get; set; }

        [Column("weight")]
        public decimal Weight { get; set; }

        [Column("volume")]
        public decimal Volume { get; set; }

        [Column("type")]
        public string Type { get; set; }
    }


    public class Tracking
    {
        [Key]
        [Column("tracking_id")]
        public int TrackingId { get; set; }

        [Column("shipment_id")]
        public int ShipmentId { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }

    public class AddCustomerViewModel
    {
        [Required]
        [Display(Name = "Name")]
        [Column("name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Address")]
        [Column("address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "Phone")]
        [Column("phone")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Company")]
        [Column("company_id")]
        public int CompanyId { get; set; }

        public IEnumerable<SelectListItem> Companies { get; set; }
    }


    public class ShipmentViewModel
        {
            [Column("shipment_id")]
            public int ShipmentId { get; set; }

            [Column("transport_vehicle_type")]
            public string TransportVehicleType { get; set; }

            [Column("route_source")]
            public string RouteSource { get; set; }

            [Column("route_destination")]
            public string RouteDestination { get; set; }

            [Column("company_name")]
            public string CompanyName { get; set; }
        }




}
