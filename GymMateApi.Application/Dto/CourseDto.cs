﻿namespace GymMateApi.Application.Dto;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double AverageRating { get; set; }

//    public ICollection<UserDto> Subscribers { get; set; } = [];
    public ICollection<CourseTrainingDto> Trainings { get; set; } = [];
}