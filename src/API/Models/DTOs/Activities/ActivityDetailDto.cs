using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Activities;

public record ActivityDetailDto(
    int? Id,
    
    [Required(ErrorMessage = "Schedule reference is Required")]
    int ScheduleId,
    
    [Required(ErrorMessage = "Activity name is required")]
    string Name,
    
    [Required(ErrorMessage = "Activity complete status is required")]
    bool IsComplete,
    
    DateTime CreatedAt,
    
    DateTime LastModified
);
    