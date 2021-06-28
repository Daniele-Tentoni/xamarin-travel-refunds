using System;
using System.Collections.Generic;

namespace TravelRefunds.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
    public class ActualEnd
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class ActualStart
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Point
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Address
    {
        public string adminDistrict { get; set; }
        public string adminDistrict2 { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
    }

    public class GeocodePoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        public string calculationMethod { get; set; }
        public List<string> usageTypes { get; set; }
    }

    public class EndLocation
    {
        public List<double> bbox { get; set; }
        public string name { get; set; }
        public Point point { get; set; }
        public Address address { get; set; }
        public string confidence { get; set; }
        public string entityType { get; set; }
        public List<GeocodePoint> geocodePoints { get; set; }
        public List<string> matchCodes { get; set; }
    }

    public class Shield
    {
        public List<string> labels { get; set; }
        public int roadShieldType { get; set; }
    }

    public class RoadShieldRequestParameters
    {
        public int bucket { get; set; }
        public List<Shield> shields { get; set; }
    }

    public class Detail
    {
        public int compassDegrees { get; set; }
        public List<int> endPathIndices { get; set; }
        public string maneuverType { get; set; }
        public string mode { get; set; }
        public List<string> names { get; set; }
        public string roadType { get; set; }
        public List<int> startPathIndices { get; set; }
        public List<string> locationCodes { get; set; }
        public RoadShieldRequestParameters roadShieldRequestParameters { get; set; }
    }

    public class Instruction
    {
        public object formattedText { get; set; }
        public string maneuverType { get; set; }
        public string text { get; set; }
    }

    public class ManeuverPoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
    }

    public class Hint
    {
        public object hintType { get; set; }
        public string text { get; set; }
    }

    public class Warning
    {
        public string origin { get; set; }
        public string severity { get; set; }
        public string text { get; set; }
        public string to { get; set; }
        public string warningType { get; set; }
    }

    public class ItineraryItem
    {
        public string compassDirection { get; set; }
        public List<Detail> details { get; set; }
        public string exit { get; set; }
        public string iconType { get; set; }
        public Instruction instruction { get; set; }
        public ManeuverPoint maneuverPoint { get; set; }
        public string sideOfStreet { get; set; }
        public string tollZone { get; set; }
        public string towardsRoadName { get; set; }
        public string transitTerminus { get; set; }
        public double travelDistance { get; set; }
        public int travelDuration { get; set; }
        public string travelMode { get; set; }
        public List<Hint> hints { get; set; }
        public List<string> signs { get; set; }
        public List<Warning> warnings { get; set; }
    }

    public class EndWaypoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        public string description { get; set; }
        public bool isVia { get; set; }
        public string locationIdentifier { get; set; }
        public int routePathIndex { get; set; }
    }

    public class StartWaypoint
    {
        public string type { get; set; }
        public List<double> coordinates { get; set; }
        public string description { get; set; }
        public bool isVia { get; set; }
        public string locationIdentifier { get; set; }
        public int routePathIndex { get; set; }
    }

    public class RouteSubLeg
    {
        public EndWaypoint endWaypoint { get; set; }
        public StartWaypoint startWaypoint { get; set; }
        public double travelDistance { get; set; }
        public int travelDuration { get; set; }
    }

    public class StartLocation
    {
        public List<double> bbox { get; set; }
        public string name { get; set; }
        public Point point { get; set; }
        public Address address { get; set; }
        public string confidence { get; set; }
        public string entityType { get; set; }
        public List<GeocodePoint> geocodePoints { get; set; }
        public List<string> matchCodes { get; set; }
    }

    public class RouteLeg
    {
        public ActualEnd actualEnd { get; set; }
        public ActualStart actualStart { get; set; }
        public List<object> alternateVias { get; set; }
        public int cost { get; set; }
        public string description { get; set; }
        public EndLocation endLocation { get; set; }
        public List<ItineraryItem> itineraryItems { get; set; }
        public string routeRegion { get; set; }
        public List<RouteSubLeg> routeSubLegs { get; set; }
        public StartLocation startLocation { get; set; }
        public double travelDistance { get; set; }
        public int travelDuration { get; set; }
    }

    public class Resource
    {
        public string __type { get; set; }
        public List<double> bbox { get; set; }
        public string id { get; set; }
        public string distanceUnit { get; set; }
        public string durationUnit { get; set; }
        public List<RouteLeg> routeLegs { get; set; }
        public double travelDistance { get; set; }
        public int travelDuration { get; set; }
        public int travelDurationTraffic { get; set; }
    }

    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public List<Resource> resources { get; set; }
    }

    public class Root
    {
        public string authenticationResultCode { get; set; }
        public string brandLogoUri { get; set; }
        public string copyright { get; set; }
        public IEnumerable<ResourceSet> resourceSets { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string traceId { get; set; }
    }
}