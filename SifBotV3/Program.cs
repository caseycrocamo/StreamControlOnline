using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using System.Net.Http;
using Newtonsoft.Json;
using DSharpPlus.Entities;
using DSharpPlus;

namespace Sifbot
{
    class Program
    {
        //user and pass for challonge
        public static string challonge_api_key = "Dd8QMcIxdxUZ2qi3OwDfZnFOvydlG3UBoVA7Rmkz";
        public static string challongeUser = "sifcif";

        //create handler to authenticate with challonge api
        static HttpClientHandler handler = new HttpClientHandler()
        {
            PreAuthenticate = true,
            Credentials = new NetworkCredential(challongeUser, challonge_api_key)
        };

        //instantiate http client with handler to communicate with challonge api
        public static HttpClient challonge = new HttpClient(handler) { BaseAddress = new Uri("https://api.challonge.com/v1/") };


        //initialize commandsnextmodule to handle discord commands
        public static CommandsNextModule Commands { get; private set; }

        //uris for current challonge tournament: upper and lower bracket
        public static string Tournament = "darkroottest";
        public static string UpperBracketLink = "<defaultupper>";
        public static string LowerBracketLink = "<defaultlower>";

        public Participant[] ParticipantsUpper;
        public Participant[] ParticipantsLower;

        static void Main(string[] args)
        {
            ConnectChallonge().GetAwaiter().GetResult();
            Run().GetAwaiter().GetResult();
        }

        public static async Task Run()
        {
            Console.WriteLine("Loading...");
            //instantiate discord object and set config
            var discord = new DiscordClient(new DiscordConfiguration
            {
                AutoReconnect = true,
                LargeThreshold = 250,
                Token = "MzI3NjI2NjE1MDg3NTYyNzUy.DC4Fsg.byuX04Ww5gZySq9UVuO3dbIJpeY",
                TokenType = TokenType.Bot,
                UseInternalLogHandler = false
            });

            //update console with discord server response
            discord.DebugLogger.LogMessageReceived += (o, e) =>
            {
                Console.WriteLine($"[{e.Timestamp}] [{e.Application}] [{e.Level}] {e.Message}");
            };

            //waits until bot connection is established
            await discord.ConnectAsync();
            PrintConsole("Connected!");

            //updates console with information about current discord guild
            discord.GuildAvailable += e =>
            {
                discord.DebugLogger.LogMessage(LogLevel.Info, "discord bot", $"Guild available: {e.Guild.Name}", DateTime.Now);
                return Task.Delay(0);
            };

            //responds to discord message "ping" with discord message "pong"
            //used to check that bot has connected to the discord server, logged in, and responsive
            discord.MessageCreated += async e =>
            {
                if (e.Message.Content.ToLower() == "ping")
                    await Respond(e, "pong");
            };


            //set command config
            var ccfg = new CommandsNextConfiguration
            {
                StringPrefix = "!",
                SelfBot = false,
                CaseSensitive = false,
                EnableMentionPrefix = true,
                EnableDefaultHelp = true
            };
            Commands = discord.UseCommandsNext(ccfg);

            //report command events to console
            Commands.CommandExecuted += Commands_CommandExecuted;
            Commands.CommandErrored += Commands_CommandErrored;

            //commands

            Commands.RegisterCommands<CommandLoader>();

            //keeps the project running
            await Task.Delay(-1);

        }

        private static async Task Commands_CommandErrored(CommandErrorEventArgs e)
        {
            PrintConsole($"{e.Context.User.Username} tried to execute {e.Command?.QualifiedName ?? "<unknown command>"} at {DateTime.Now}, but it failed and threw exception: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}.");

            if (e.Exception is ChecksFailedException ex)
            {
                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");
                await e.Context.RespondAsync($"{emoji} You do not have the permissions required to execute this command.");
            }
            else
            {
                await e.Context.RespondAsync("Command failed or does not exist, respond with !help for the command list");
            }
        }

        private static async Task Commands_CommandExecuted(CommandExecutionEventArgs e)
        {
            PrintConsole($"{e.Context.User.Username} successfuly executed {e.Command.QualifiedName} at {DateTime.Now}");
        }

        public static void PrintConsole(string s)
        {
            Console.WriteLine(s);
        }

        static async Task Respond(MessageCreateEventArgs e, string s)
        {
            await e.Message.RespondAsync(s);

            String s2 = "message sent to text channel: <" + e.Message.Channel.Name + "> in response to user: <" + e.Message.Author.Username + "> ---> " + s;
            PrintConsole(s2);
        }

        static async Task ConnectChallonge()
        {
            PrintConsole("connecting to challonge api...");
            challonge.DefaultRequestHeaders.Accept.Clear();
            challonge.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            PrintConsole("connected!");
        }

        public static async Task<HttpResponseMessage> AddParticipant(Participant p, string tournamentUri)
        {
            var content = CreateByteContent(p);
            HttpResponseMessage response = await challonge.PostAsync($"tournaments/{tournamentUri}/participants.json", content);
            return response;
        }

        public static async Task<List<ParticipantJson>> GetParticipants(string tournamentUri)
        {
            HttpResponseMessage response = await challonge.GetAsync($"tournaments/{tournamentUri}/participants.json");
            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                PrintConsole("Succesfully received participant list!");
                List<ParticipantJson> participantList = JsonConvert.DeserializeObject<List<ParticipantJson>>(json);
                return participantList;

            }
            else
            {
                PrintConsole($"Did not succesfuly receive participant list, {response.ToString()}");
                return null;
            }
        }

        public static async Task RemoveParticipant(Participant p, string tournamentUri)
        {
            HttpResponseMessage response = await challonge.DeleteAsync($"tournaments/{tournamentUri}/participants/{p.Id}.json");
        }

        public static ByteArrayContent CreateByteContent(Participant p)
        {
            var content = JsonConvert.SerializeObject(p);
            var buffer = Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }

    }
    public class CommandLoader
    {

        public string UpperBracketLink = "<defaultupper>";
        public string LowerBracketLink = "<defaultlower>";

        [Command("greet"), Description("Says hi to specified user."), Aliases("sayhi", "say_hi")]
        public async Task Greet(CommandContext ctx, DiscordMember member)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DSharpPlus.Entities.DiscordEmoji.FromName(ctx.Client, ":wave:");
            await ctx.RespondAsync($"{emoji} Hello, {member.Mention}!");
        }

        [Command("bracket"), Description("provides link to the current bracket for both devisions hosted on Challonge")]
        public async Task Bracket(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            await ctx.RespondAsync($"The upper bracket can be found at: {UpperBracketLink}");
            await ctx.RespondAsync($"The lower bracket can be found at: {LowerBracketLink}");
        }

        [Command("join"), Description("join the upcoming tournament"), Aliases("Enter")]
        public async Task Join(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var _name = ctx.Member.Nickname;
            Participant p = new Participant()
            {
                Name = _name,
                Seed = "1"
            };
            
            HttpResponseMessage response = new HttpResponseMessage();
            response = await Program.AddParticipant(p, Program.Tournament);
            if (response.IsSuccessStatusCode)
            {
                Program.PrintConsole($"{ctx.Member.Nickname} has joined the upcoming tournament {Program.Tournament} in the upper bracket");
                await ctx.RespondAsync($"{ctx.Member.Nickname} has joined the upcoming tournament {Program.Tournament} in the upper bracket");
            }

            else
            {
                Program.PrintConsole($"{ctx.Member.Nickname} has attempted to join the upcoming tournament {Program.Tournament} but failed due to {response}");
                await ctx.RespondAsync($"{ctx.Member.Mention} joining tournament {Program.Tournament} has failed, make sure that you are not already signed up then contact a TO (@Tournament Organizers (TOS))");
            }

        }

        [Command("leave"), Description("removes user from the upcoming tournament"), Aliases("quit")]
        public async Task Leave(CommandContext ctx)
        {
            bool deleted = false;
            List<ParticipantJson> participantList = null;
            participantList = await Program.GetParticipants(Program.Tournament);
            for (int i = 0; i < participantList.Count; i++)
            {
                if (participantList[i].Participant.Name == ctx.Member.Nickname)
                {
                    await Program.RemoveParticipant(participantList[i].Participant, Program.Tournament);
                    deleted = true;
                }
            }
            if (deleted)
            {
                await ctx.RespondAsync($"{ctx.Member.Nickname} has been removed from {Program.Tournament}");
            }
            else
            {
                await ctx.RespondAsync($"removal has failed, please check to be sure that you are registered for the tournament with the same name as your discord nickname, then contact @Tournament Organizers (TOs)");
            }
        }

    }
    public class Participant
    {
        public Participant() { }

        public Participant(string _name, string _seed)
        {
            this.Name = _name;
            this.Seed = _seed;
        }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("seed")]
        public string Seed { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }


    }
    public class ParticipantJson
    {
        [JsonProperty("participant")]
        public Participant Participant { get; set; }
    }

}
