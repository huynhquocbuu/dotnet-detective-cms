namespace Configuration.Persistence.Interfaces;

public interface IUserTracking
{
    string CreatedBy { get; set; }
    string LastModifiedBy { get; set; }
}