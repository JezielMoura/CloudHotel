using static System.StringComparison;

namespace CloudHotel.Domain.ReservationAggregate;

public sealed class ReservationStatus(string name, int value, string description) : Enumeration<ReservationStatus>(name, value)
{
    private static readonly Dictionary<int, ReservationStatus> _status = new()
    {
        { 0, new(name: nameof(Pendent), value: 0, description: "Pendente de Confirmação") },
        { 1, new(name: nameof(Confirmed), value: 1, description: "Confirmada") },
        { 2, new(name: nameof(InHouse), value: 2, description: "Hospedado") },
        { 3, new(name: nameof(Completed), value: 3, description: "Check-out realizado") },
        { 4, new(name: nameof(Canceled), value: 4, description: "Cancelada") }
    };

    public static readonly ReservationStatus Pendent = _status[0];
    public static readonly ReservationStatus Confirmed = _status[1];
    public static readonly ReservationStatus InHouse = _status[2];
    public static readonly ReservationStatus Completed = _status[3];
    public static readonly ReservationStatus Canceled = _status[4];

    public string Description { get; } = description;

    public static ReservationStatus FromNumber(int id) => 
        _status.TryGetValue(id, out var value) ? value : throw new InvalidOperationException();

    public static ReservationStatus FromName(string name) =>
        _status.FirstOrDefault(x => x.Value.Name.Equals(name, OrdinalIgnoreCase)).Value ?? throw new InvalidOperationException();

    public static IEnumerable<ReservationStatus> GetAll() =>
        _status.Select(x => x.Value);
}