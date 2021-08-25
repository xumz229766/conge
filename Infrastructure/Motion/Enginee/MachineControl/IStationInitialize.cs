namespace CMotion.Applications
{
    public interface IStationInitialize
    {
        bool InitializeDone { get; set; }
        bool Running { get; }
        InilitiazeStatus Status { get;}
        int Flow { get; set; }
    }
}
