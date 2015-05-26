// include all tests
[assembly: Dot42.Include(Pattern = "Apply to type *Tests: Dot42.IncludeType")]

// include all test objects
[assembly: Dot42.Include(Pattern = "Apply to type Newtonsoft.Json.Tests.*: Dot42.IncludeType")]
