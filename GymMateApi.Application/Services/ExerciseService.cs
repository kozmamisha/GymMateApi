using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;

namespace GymMateApi.Application.Services
{
    public class ExerciseService(
        IExerciseRepository exerciseRepository,
        ITrainingRepository trainingRepository) : IExerciseService
    {
        public async Task CreateAsync(string name, string description, Guid trainingId, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Exercise name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new BadRequestException("Exercise description cannot be empty");
            
            var training = await trainingRepository.GetTrainingById(trainingId, cancellationToken)
                ?? throw new EntityNotFoundException("Training not found");

            ExerciseEntity exercise = new()
            {
                Name = name,
                Description = description,
                TrainingId = trainingId
            };

            await exerciseRepository.CreateExercise(exercise, cancellationToken);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var exercise = await exerciseRepository.GetExerciseById(id, cancellationToken)
                ?? throw new EntityNotFoundException("This exercise not found");

            await exerciseRepository.DeleteExercise(exercise, cancellationToken);
        }

        public async Task<List<ExerciseDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var exercises = await exerciseRepository.GetAllExercises(cancellationToken);

            return exercises.ToDtoList();
        }

        public async Task<List<ExerciseDto>> GetByPage(int page, int pageSize, CancellationToken cancellationToken)
        {
            var exercises = await exerciseRepository.GetExercisesByPage(page, pageSize, cancellationToken);

            return exercises.ToDtoList();
        }

        public async Task<ExerciseDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var exercise = await exerciseRepository.GetExerciseById(id, cancellationToken)
                ?? throw new EntityNotFoundException("This exercise not found");

            return exercise.ToDto();
        }

        public async Task UpdateAsync(Guid id, string name, string description, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Exercise name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new BadRequestException("Exercise description cannot be empty");

            var currentExercise = await exerciseRepository.GetExerciseById(id, cancellationToken)
                ?? throw new EntityNotFoundException("This exercise not found");

            currentExercise.Name = name;
            currentExercise.Description = description;

            await exerciseRepository.UpdateExercise(currentExercise, cancellationToken);
        }
    }
}
