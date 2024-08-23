using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json;

public class Flight
{
    public int FlightNumber { get; set; }
    public string DepartureCity { get; set; }
    public string ArrivalCity { get; set; }
    public int Day { get; set; }
}

public class FlightSchedule
{
    private List<Flight> flights;

    public FlightSchedule()
    {
        flights = new List<Flight>();
    }

    public void LoadFlights()
    {
        flights.Add(new Flight { FlightNumber = 1, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 });
        flights.Add(new Flight { FlightNumber = 2, DepartureCity = "YYZ", ArrivalCity = "YUL", Day = 1 });
        flights.Add(new Flight { FlightNumber = 3, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 1 });
        flights.Add(new Flight { FlightNumber = 4, DepartureCity = "YYZ", ArrivalCity = "YUL", Day = 2 });
        flights.Add(new Flight { FlightNumber = 5, DepartureCity = "YUL", ArrivalCity = "YYZ", Day = 2 });
        flights.Add(new Flight { FlightNumber = 6, DepartureCity = "YYZ", ArrivalCity = "YUL", Day = 2 });
    }

    public void ListFlights()
    {
        foreach (var flight in flights)
        {
            Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.DepartureCity}, arrival: {flight.ArrivalCity}, day: {flight.Day}");
        }
    }
}

public class Order
{
    public int OrderId { get; set; }
    public string Product { get; set; }
    public int Quantity { get; set; }
}

public class FlightItinerary
{
    public List<Order> Orders { get; set; }
    public int TotalCapacity { get; set; }

    public FlightItinerary(List<Order> orders, int totalCapacity)
    {
        Orders = orders;
        TotalCapacity = totalCapacity;
    }
}
public class InventoryManager
{
    public static List<Order> LoadOrdersFromJson(string filePath)
    {
        var jsonData = File.ReadAllText(filePath);
        return JsonConvert.DeserializeObject<List<Order>>(jsonData);
    }

    public static FlightItinerary GenerateFlightItinerary(List<Order> orders, int capacity)
    {
        var selectedOrders = new List<Order>();
        int currentCapacity = 0;

        foreach (var order in orders)
        {
            if (currentCapacity + order.Quantity <= capacity)
            {
                selectedOrders.Add(order);
                currentCapacity += order.Quantity;
            }
            else
            {
                break;
            }
        }

        return new FlightItinerary(selectedOrders, currentCapacity);
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        #region Scenario1
        //Solution1
        FlightSchedule flightSchedule = new FlightSchedule();
        flightSchedule.LoadFlights();
        flightSchedule.ListFlights();
        #endregion
        #region Scenario2
        string filePath = @"D:\\AirTekTestProject\\AirTekTestProject\\StaticFiles\\coding-assigment-orders.json";
        int flightCapacity = 100;

        var orders = InventoryManager.LoadOrdersFromJson(filePath);
        var flightItinerary = InventoryManager.GenerateFlightItinerary(orders, flightCapacity);

        Console.WriteLine($"Total Capacity Used: {flightItinerary.TotalCapacity}");
        foreach (var order in flightItinerary.Orders)
        {
            Console.WriteLine($"Order ID: {order.OrderId}, Product: {order.Product}, Quantity: {order.Quantity}");
        }
        #endregion
    }

    //Solution2
    void LoadFlightDetails()
    {
        string jsonArray = "[{\"FlightNumber\": 1,\"DepartureCity\":\"YUL\",\"ArrivalCity\":\"YYZ\",\"Day\":1 }, {\"FlightNumber\": 2,\"DepartureCity\":\"YUL\",\"ArrivalCity\":\"YYC\",\"Day\":1 }, {\"FlightNumber\": 3,\"DepartureCity\":\"YUL\",\"ArrivalCity\":\"YYZ\",\"Day\":2 }, {\"FlightNumber\": 4,\"DepartureCity\":\"YUL\",\"ArrivalCity\":\"YYC\",\"Day\":2 }]";
        var flightList = System.Text.Json.JsonSerializer.Deserialize<IList<Flight>>(jsonArray);

        foreach (var flight in flightList)
        {
            Console.WriteLine($"Flight: {flight.FlightNumber}, departure: {flight.DepartureCity}, arrival: {flight.ArrivalCity}, day: {flight.Day}");
        }
    }
}
