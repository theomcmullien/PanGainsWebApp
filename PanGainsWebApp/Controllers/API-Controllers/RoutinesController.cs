#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PanGainsWebApp.Data;
using PanGainsWebApp.Models;

namespace PanGainsWebApp.Controllers.API_Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RoutinesController : ControllerBase
    {
        private readonly PanGainsWebAppContext _context;

        public RoutinesController(PanGainsWebAppContext context)
        {
            _context = context;
        }

        // GET: api/Routines
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Routine>>> GetRoutine()
        {
            return await _context.Routine.ToListAsync();
        }

        // GET: api/Routines/5
        [HttpGet("{folderID}")]
        public async Task<ActionResult<IEnumerable<RoutineWithExercises>>> GetRoutine(int folderID)
        {
            List<RoutineWithExercises> list = new List<RoutineWithExercises>();

            var routinesList = await _context.Routine.ToListAsync();
            List<Routine> routines = routinesList.Where(r => r.FolderID == folderID).ToList();

            foreach (Routine routine in routines)
            {
                RoutineWithExercises r = new RoutineWithExercises();
                r.RoutineID = routine.RoutineID;
                r.RoutineName = routine.RoutineName;

                var yourExercisesList = await _context.YourExercise.ToListAsync();
                List<YourExercise> yourExercises = yourExercisesList.Where(y => y.RoutineID == routine.RoutineID).ToList();
                var exercisesList = await _context.Exercise.ToListAsync();

                List<string> exercises = new List<string>();

                foreach (YourExercise y in yourExercises)
                {
                    foreach (Exercise e in exercisesList)
                    {
                        if (y.ExerciseID == e.ExerciseID) exercises.Add(e.ExerciseName);
                    }
                }

                r.Exercises = exercises;
                list.Add(r);
            }

            if (list == null) return NotFound();

            return list;
        }

        // PUT: api/Routines/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoutine(int id, Routine routine)
        {
            if (id != routine.RoutineID) return BadRequest();

            _context.Entry(routine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoutineExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        // POST: api/Routines
        [HttpPost]
        public async Task<ActionResult<Routine>> PostRoutine(Routine routine)
        {
            _context.Routine.Add(routine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRoutine", new { id = routine.RoutineID }, routine);
        }

        // DELETE: api/Routines/5
        [HttpDelete("{routineID}")]
        public async Task<IActionResult> DeleteRoutine(int routineID)
        {
            //remove CompletedWorkout
            IEnumerable<CompletedWorkout> completedWorkoutsList = await _context.CompletedWorkout.ToListAsync();
            List<CompletedWorkout> completedWorkouts = completedWorkoutsList.Where(c => c.RoutineID == routineID).ToList();
            if (completedWorkouts.Any()) foreach (CompletedWorkout c in completedWorkouts) _context.CompletedWorkout.Remove(c);

            //remove YourExercise
            IEnumerable<YourExercise> yourExerciseslist = await _context.YourExercise.ToListAsync();
            List<YourExercise> yourExercises = yourExerciseslist.Where(y => y.RoutineID == routineID).ToList();
            if (yourExercises.Any()) foreach (YourExercise y in yourExercises) _context.YourExercise.Remove(y);

            //remove Routine
            Routine routine = await _context.Routine.FindAsync(routineID);
            if (routine == null) return NotFound();
            _context.Routine.Remove(routine);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RoutineExists(int id)
        {
            return _context.Routine.Any(e => e.RoutineID == id);
        }
    }
}
