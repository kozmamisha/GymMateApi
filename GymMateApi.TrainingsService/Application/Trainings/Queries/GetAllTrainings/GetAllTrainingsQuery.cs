using GymMateApi.TrainingsService.Application.Dto;
using MediatR;

namespace GymMateApi.TrainingsService.Application.Trainings.Queries.GetAllTrainings;

public record GetAllTrainingsQuery() : IRequest<List<TrainingDto>>;
