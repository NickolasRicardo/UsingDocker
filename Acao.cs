using Hangfire;

namespace Hangfire_Teste
{
    public static class Acao
    {
        public static bool running = false;

        public static void RegistrarMensagem(string tipo)
        {
         //   System.IO.File.AppendAllText(@"C:\temp\loghang.txt", Environment.NewLine + $"EXECUTOU {tipo} {DateTime.Now}");
            Console.WriteLine($"EXECUTOU {tipo} {DateTime.Now}");
        }
        public static void RegistrarMensagem2(string tipo)
        {
            if (!running) { 
                running = true;
                System.IO.File.AppendAllText(@"C:\temp\loghang.txt", Environment.NewLine + $"EXECUTOU {tipo} {DateTime.Now}");
                Console.WriteLine($"EXECUTOU {tipo} {DateTime.Now}");
                Thread.Sleep( 120000 );
                Console.WriteLine($"Finalizou {tipo} {DateTime.Now}");
                running = false;
            }

        }

        public static string primeiro()
        {
            return BackgroundJob.Schedule(() => Acao.RegistrarMensagem("primeiro"), TimeSpan.FromSeconds(2));

        }

        public static void segundo()
        {
            var jobDelayed = BackgroundJob.Schedule(() => Acao.RegistrarMensagem("segundo"), TimeSpan.FromSeconds(2));
            BackgroundJob.ContinueJobWith(jobDelayed, () => terceiro());
        }

        public static void terceiro()
        {
            BackgroundJob.ContinueJobWith(primeiro(), () => segundo());
        }
    }
}
