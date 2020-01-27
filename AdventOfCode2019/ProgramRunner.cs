namespace AdventOfCode2019
{
    public class ProgramRunner
    {
        public void Run<TProgram>() where TProgram : ProgramBase, new()
        {
            IProgram problem = new TProgram();

            Solve(problem);
        }

        protected virtual void Solve(IProgram problem)
        {
            problem.Solve();
        }
    }
}
