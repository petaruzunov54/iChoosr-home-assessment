using System.Text.Json.Serialization;

namespace iChoosr_home_assessment.PayloadModels
{
    public class OrbitParams
    {
        [JsonPropertyName("reference_system")]
        public string? ReferenceSystem { get; set; }

        [JsonPropertyName("regime")]
        public string? Regime { get; set; }

        [JsonPropertyName("longitude")]
        public double? Longitude { get; set; }

        [JsonPropertyName("semi_major_axis_km")]
        public double? SemiMajorAxisKm { get; set; }

        [JsonPropertyName("eccentricity")]
        public double? Eccentricity { get; set; }

        [JsonPropertyName("periapsis_km")]
        public double? PeriapsisKm { get; set; }

        [JsonPropertyName("apoapsis_km")]
        public double? ApoapsisKm { get; set; }

        [JsonPropertyName("inclination_deg")]
        public double? InclinationDeg { get; set; }

        [JsonPropertyName("period_min")]
        public double? PeriodMin { get; set; }

        [JsonPropertyName("lifespan_years")]
        public double? LifespanYears { get; set; }

        [JsonPropertyName("epoch")]
        public DateTime? Epoch { get; set; }

        [JsonPropertyName("mean_motion")]
        public double? MeanMotion { get; set; }

        [JsonPropertyName("raan")]
        public double? Raan { get; set; }

        [JsonPropertyName("arg_of_pericenter")]
        public double? ArgOfPericenter { get; set; }

        [JsonPropertyName("mean_anomaly")]
        public double? MeanAnomaly { get; set; }
    }

    public class Payload
    {
        [JsonPropertyName("payload_id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("payload_type")]
        public string? Type { get; set; }

        [JsonPropertyName("reused")]
        public bool? Reused { get; set; }

        [JsonPropertyName("customers")]
        public List<string>? Customers { get; set; }

        [JsonPropertyName("nationality")]
        public string? Nationality { get; set; }

        [JsonPropertyName("manufacturer")]
        public string? Manufacturer { get; set; }

        [JsonPropertyName("payload_mass_kg")]
        public double? MassKg { get; set; }

        [JsonPropertyName("payload_mass_lbs")]
        public double? MassLbs { get; set; }

        [JsonPropertyName("orbit")]
        public string? Orbit { get; set; }

        [JsonPropertyName("orbit_params")]
        public OrbitParams? OrbitParams { get; set; }
    }
}
