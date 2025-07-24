using GymMateApi.Application.Exceptions;
using GymMateApi.Application.Interfaces;
using GymMateApi.Core.Entities;
using GymMateApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymMateApi.Application.Dto;
using GymMateApi.Application.Extensions;

namespace GymMateApi.Application.Services
{
    public class ExerciseService(IExerciseRepository exerciseRepository, ITrainingRepository trainingRepository) : IExerciseService
    {
        public async Task CreateAsync(string name, string description, Guid trainingId)
        {
            var training = await trainingRepository.GetTrainingById(trainingId)
                ?? throw new EntityNotFoundException("Training not found");

            ExerciseEntity exercise = new()
            {
                Name = name,
                Description = description,
                TrainingId = trainingId
            };

            await exerciseRepository.CreateExercise(exercise);
        }

        public async Task DeleteAsync(Guid id)
        {
            var exercise = await exerciseRepository.GetExerciseById(id)
                ?? throw new EntityNotFoundException("This exercise not found");

            await exerciseRepository.DeleteExercise(exercise);
        }

        public async Task<List<ExerciseDto>> GetAllAsync()
        {
            var exercises = await exerciseRepository.GetAllExercises();

            return exercises.ToDtoList();
        }

        public async Task<ExerciseDto?> GetByIdAsync(Guid id)
        {
            var exercise = await exerciseRepository.GetExerciseById(id)
                ?? throw new EntityNotFoundException("This exercise not found");

            return exercise.ToDto();
        }

        public async Task UpdateAsync(Guid id, string name, string description)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new BadRequestException("Exercise name cannot be empty");

            if (string.IsNullOrWhiteSpace(description))
                throw new BadRequestException("Exercise description cannot be empty");

            var currentExercise = await exerciseRepository.GetExerciseById(id)
                ?? throw new EntityNotFoundException("This exercise not found");

            currentExercise.Name = name;
            currentExercise.Description = description;

            await exerciseRepository.UpdateExercise(currentExercise);
        }
    }
}
