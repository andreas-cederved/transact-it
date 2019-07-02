using System.Reflection;

namespace TransactIt.Application.Write.Helpers
{
    public static class WriteAssemblyHelper
    {
        public static Assembly Get()
        {
            return Assembly.GetAssembly(typeof(WriteAssemblyHelper));
        }
    }
}
