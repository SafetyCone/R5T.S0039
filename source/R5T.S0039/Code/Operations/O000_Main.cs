using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using R5T.Lombardy;

using R5T.D0083;
using R5T.D0105;

using R5T.T0020;


namespace R5T.S0039
{
    /// <summary>
    /// Jarlshof functionality, listing all recursive dependencies of a project (and output to a file).
    /// </summary>
    public class O000_Main : IActionOperation
    {
        private INotepadPlusPlusOperator NotepadPlusPlusOperator { get; }
        private IStringlyTypedPathOperator StringlyTypedPathOperator { get; }
        private IVisualStudioProjectFileReferencesProvider VisualStudioProjectFileReferencesProvider { get; }


        public O000_Main(
            INotepadPlusPlusOperator notepadPlusPlusOperator,
            IStringlyTypedPathOperator stringlyTypedPathOperator,
            IVisualStudioProjectFileReferencesProvider visualStudioProjectFileReferencesProvider)
        {
            this.NotepadPlusPlusOperator = notepadPlusPlusOperator;
            this.StringlyTypedPathOperator = stringlyTypedPathOperator;
            this.VisualStudioProjectFileReferencesProvider = visualStudioProjectFileReferencesProvider;
        }

        public async Task Run()
        {
            /// Inputs.
            var projectFilePath = @"C:\Code\DEV\Git\GitHub\SafetyCone\R5T.T0041\source\R5T.T0041.X002\R5T.T0041.X002.csproj";

            var outputFilePath = @"C:\Temp\Project References-Recursive.txt";

            /// Run.
            // Get recursive project references for project.
            var recursiveProjectReferences = await Instances.ProjectReferencesOperator.GetRecursiveProjectReferences(
                projectFilePath,
                this.VisualStudioProjectFileReferencesProvider.GetProjectReferencesForProject);

            // Format output.
            var projectFilePathsByProjectName = recursiveProjectReferences
                .ToDictionary(
                    xProjectFilePath =>
                    {
                        var projectFileNameWithoutExtension = this.StringlyTypedPathOperator.GetFileNameWithoutExtension(xProjectFilePath);
                        return projectFileNameWithoutExtension;
                    });

            var maxProjectNameLength = projectFilePathsByProjectName.Keys
                .Select(x => x.Length)
                .Max();

            var projectNameColumnWidth = maxProjectNameLength + 3;

            var recursiveProjectReferencesCount = recursiveProjectReferences.Length;

            var outputLines = EnumerableHelper.From(
                $"Recursive project references for project:\n{projectFilePath}\n\nCount: ({recursiveProjectReferencesCount})\n")
                .Append(recursiveProjectReferences
                    .Select(xProjectFilePath =>
                    {
                        var projectFileNameWithoutExtension = this.StringlyTypedPathOperator.GetFileNameWithoutExtension(xProjectFilePath);

                        var line = $"{projectFileNameWithoutExtension.PadRight(projectNameColumnWidth)}{xProjectFilePath}";
                        return line;
                    })
                    .OrderAlphabetically());

            // Write output to file.
            await FileHelper.WriteAllLines(
                outputFilePath,
                outputLines);

            // Show output file.
            await this.NotepadPlusPlusOperator.OpenFilePath(outputFilePath);
        }
    }
}
