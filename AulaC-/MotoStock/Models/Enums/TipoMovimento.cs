namespace MotoStock.Models.Enums
{
    /// <summary>
    /// Tipos de movimento de stock permitidos.
    /// </summary>
    public enum TipoMovimento
    {
        Entrada = 1,
        Saída = 2,
        Reserva = 3,
        CancelamentoReserva = 4
    }
}
