namespace Sohg.Grids2D.Contracts
{
    public interface ICell
    {
        bool IsSocietyAssigned { get; }
        bool IsSocietyUnassigned { get; }

        void Initialize(int cellIndex);
        void SetSocietyAssigned();
    }
}
