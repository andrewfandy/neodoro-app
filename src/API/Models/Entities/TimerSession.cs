namespace API.Models.Entities;

public class TimerSession
{
    public int Id { get; set; }
    
    public required Schedule Schedule { get; set; } // one-to-one with Schedule

    public required TimeSpan FocusDuration { get; set; } = new TimeSpan(1, 0, 0);
    
    private TimeSpan? _restDuration; // helper method of RestDuration prop
    public required TimeSpan RestDuration
    {
        
        // default value is 10% of FocusDuration
        // or get the set value
        get
        {
            return _restDuration ?? FocusDuration.Multiply(0.1);
        }  
        
        // Other class could set value to _restDuration
        set
        {
            _restDuration = value;  
        } 
    }
    
    public TimeSpan TotalDuration => FocusDuration.Add(RestDuration);
}