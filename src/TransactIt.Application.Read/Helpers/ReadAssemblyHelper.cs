using System.Reflection;

namespace TransactIt.Application.Read.Helpers
{
    public static class ReadAssemblyHelper
    {
        public static Assembly Get()
        {
            return Assembly.GetAssembly(typeof(ReadAssemblyHelper));
        }
    }
}
