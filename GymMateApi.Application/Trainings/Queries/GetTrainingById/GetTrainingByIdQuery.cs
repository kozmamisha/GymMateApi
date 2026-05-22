using GymMateApi.Application.Dto;
using MediatR;

namespace GymMateApi.Application.Trainings.Queries.GetTrainingById;

public record GetTrainingByIdQuery(Guid Id) : IRequest<TrainingDto>;