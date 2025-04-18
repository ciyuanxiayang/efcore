﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Microsoft.EntityFrameworkCore.Query;

#nullable disable

public class FunkyDataQueryAzureSynapseTest : FunkyDataQuerySqlServerBaseTest<FunkyDataQueryAzureSynapseTest.FunkyDataQueryAzureSynapseFixture>
{
    public FunkyDataQueryAzureSynapseTest(FunkyDataQueryAzureSynapseFixture fixture, ITestOutputHelper testOutputHelper)
        : base(fixture, testOutputHelper)
    {
    }

    public override async Task String_contains_on_argument_with_wildcard_constant(bool async)
    {
        await base.String_contains_on_argument_with_wildcard_constant(async);

        AssertSql(
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND CHARINDEX(N'%B', [f].[FirstName]) > 0
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND CHARINDEX(N'a_', [f].[FirstName]) > 0
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND CHARINDEX(N'_Ba_', [f].[FirstName]) > 0
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR CHARINDEX(N'%B%a%r', [f].[FirstName]) <= 0
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_contains_on_argument_with_wildcard_parameter(bool async)
    {
        await base.String_contains_on_argument_with_wildcard_parameter(async);

        AssertSql(
            """
@prm1='%B' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND (CHARINDEX(@prm1, [f].[FirstName]) > 0 OR @prm1 LIKE N'')
""",
            //
            """
@prm2='a_' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND (CHARINDEX(@prm2, [f].[FirstName]) > 0 OR @prm2 LIKE N'')
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
            //
            """
@prm4='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND (CHARINDEX(@prm4, [f].[FirstName]) > 0 OR @prm4 LIKE N'')
""",
            //
            """
@prm5='_Ba_' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND (CHARINDEX(@prm5, [f].[FirstName]) > 0 OR @prm5 LIKE N'')
""",
            //
            """
@prm6='%B%a%r' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR (CHARINDEX(@prm6, [f].[FirstName]) <= 0 AND @prm6 NOT LIKE N'')
""",
            //
            """
@prm7='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR (CHARINDEX(@prm7, [f].[FirstName]) <= 0 AND @prm7 NOT LIKE N'')
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_contains_on_argument_with_wildcard_column(bool async)
    {
        await base.String_contains_on_argument_with_wildcard_column(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND (CHARINDEX([f0].[LastName], [f].[FirstName]) > 0 OR [f0].[LastName] LIKE N'')
""");
    }

    public override async Task String_contains_on_argument_with_wildcard_column_negated(bool async)
    {
        await base.String_contains_on_argument_with_wildcard_column_negated(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NULL OR [f0].[LastName] IS NULL OR (CHARINDEX([f0].[LastName], [f].[FirstName]) <= 0 AND [f0].[LastName] NOT LIKE N'')
""");
    }

    public override async Task String_starts_with_on_argument_with_wildcard_constant(bool async)
    {
        await base.String_starts_with_on_argument_with_wildcard_constant(async);

        AssertSql(
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'%B')) = N'%B'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'_B')) = N'_B'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'_Ba_')) = N'_Ba_'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR LEFT([f].[FirstName], LEN(N'%B%a%r')) <> N'%B%a%r'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_starts_with_on_argument_with_wildcard_parameter(bool async)
    {
        await base.String_starts_with_on_argument_with_wildcard_parameter(async);

        AssertSql(
            """
@prm1='%B' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm1)) = @prm1
""",
            //
            """
@prm2='_B' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm2)) = @prm2
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
            //
            """
@prm4='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm4)) = @prm4
""",
            //
            """
@prm5='_Ba_' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm5)) = @prm5
""",
            //
            """
@prm6='%B%a%r' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR LEFT([f].[FirstName], LEN(@prm6)) <> @prm6
""",
            //
            """
@prm7='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR LEFT([f].[FirstName], LEN(@prm7)) <> @prm7
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_starts_with_on_argument_with_bracket(bool async)
    {
        await base.String_starts_with_on_argument_with_bracket(async);

        AssertSql(
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'[')) = N'['
""",
            //
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'B[')) = N'B['
""",
            //
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(N'B[[a^')) = N'B[[a^'
""",
            //
            """
@prm1='[' (Size = 4000)

SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm1)) = @prm1
""",
            //
            """
@prm2='B[' (Size = 4000)

SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm2)) = @prm2
""",
            //
            """
@prm3='B[[a^' (Size = 4000)

SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND LEFT([f].[FirstName], LEN(@prm3)) = @prm3
""",
            //
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND [f].[LastName] IS NOT NULL AND LEFT([f].[FirstName], LEN([f].[LastName])) = [f].[LastName]
""");
    }

    public override async Task String_starts_with_on_argument_with_wildcard_column(bool async)
    {
        await base.String_starts_with_on_argument_with_wildcard_column(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND LEFT([f].[FirstName], LEN([f0].[LastName])) = [f0].[LastName]
""");
    }

    public override async Task String_starts_with_on_argument_with_wildcard_column_negated(bool async)
    {
        await base.String_starts_with_on_argument_with_wildcard_column_negated(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NULL OR [f0].[LastName] IS NULL OR LEFT([f].[FirstName], LEN([f0].[LastName])) <> [f0].[LastName]
""");
    }

    public override async Task String_ends_with_on_argument_with_wildcard_constant(bool async)
    {
        await base.String_ends_with_on_argument_with_wildcard_constant(async);

        AssertSql(
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(N'%r')) = N'%r'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(N'r_')) = N'r_'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(N'_r_')) = N'_r_'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR RIGHT([f].[FirstName], LEN(N'a%r%')) <> N'a%r%'
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL
""",
                //
                """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_ends_with_on_argument_with_wildcard_parameter(bool async)
    {
        await base.String_ends_with_on_argument_with_wildcard_parameter(async);

        AssertSql(
            """
@prm1='%r' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(@prm1)) = @prm1
""",
            //
            """
@prm2='r_' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(@prm2)) = @prm2
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE 0 = 1
""",
            //
            """
@prm4='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(@prm4)) = @prm4
""",
            //
            """
@prm5='_r_' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NOT NULL AND RIGHT([f].[FirstName], LEN(@prm5)) = @prm5
""",
            //
            """
@prm6='a%r%' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR RIGHT([f].[FirstName], LEN(@prm6)) <> @prm6
""",
            //
            """
@prm7='' (Size = 4000)

SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
WHERE [f].[FirstName] IS NULL OR RIGHT([f].[FirstName], LEN(@prm7)) <> @prm7
""",
            //
            """
SELECT [f].[FirstName]
FROM [FunkyCustomers] AS [f]
""");
    }

    public override async Task String_ends_with_on_argument_with_wildcard_column(bool async)
    {
        await base.String_ends_with_on_argument_with_wildcard_column(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND RIGHT([f].[FirstName], LEN([f0].[LastName])) = [f0].[LastName]
""");
    }

    public override async Task String_ends_with_on_argument_with_wildcard_column_negated(bool async)
    {
        await base.String_ends_with_on_argument_with_wildcard_column_negated(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE [f].[FirstName] IS NULL OR [f0].[LastName] IS NULL OR RIGHT([f].[FirstName], LEN([f0].[LastName])) <> [f0].[LastName]
""");
    }

    public override async Task String_ends_with_inside_conditional(bool async)
    {
        await base.String_ends_with_inside_conditional(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE CASE
    WHEN [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND RIGHT([f].[FirstName], LEN([f0].[LastName])) = [f0].[LastName] THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END = CAST(1 AS bit)
""");
    }

    public override async Task String_ends_with_inside_conditional_negated(bool async)
    {
        await base.String_ends_with_inside_conditional_negated(async);

        AssertSql(
            """
SELECT [f].[FirstName] AS [fn], [f0].[LastName] AS [ln]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE CASE
    WHEN [f].[FirstName] IS NULL OR [f0].[LastName] IS NULL OR RIGHT([f].[FirstName], LEN([f0].[LastName])) <> [f0].[LastName] THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END = CAST(1 AS bit)
""");
    }

    public override async Task String_ends_with_equals_nullable_column(bool async)
    {
        await base.String_ends_with_equals_nullable_column(async);

        AssertSql(
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool], [f0].[Id], [f0].[FirstName], [f0].[LastName], [f0].[NullableBool]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE CASE
    WHEN [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND RIGHT([f].[FirstName], LEN([f0].[LastName])) = [f0].[LastName] THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END = [f].[NullableBool]
""");
    }

    public override async Task String_ends_with_not_equals_nullable_column(bool async)
    {
        await base.String_ends_with_not_equals_nullable_column(async);

        AssertSql(
            """
SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool], [f0].[Id], [f0].[FirstName], [f0].[LastName], [f0].[NullableBool]
FROM [FunkyCustomers] AS [f]
CROSS JOIN [FunkyCustomers] AS [f0]
WHERE CASE
    WHEN [f].[FirstName] IS NOT NULL AND [f0].[LastName] IS NOT NULL AND RIGHT([f].[FirstName], LEN([f0].[LastName])) = [f0].[LastName] THEN CAST(1 AS bit)
    ELSE CAST(0 AS bit)
END <> [f].[NullableBool] OR [f].[NullableBool] IS NULL
""");
    }

    public override async Task String_FirstOrDefault_and_LastOrDefault(bool async)
    {
        await base.String_FirstOrDefault_and_LastOrDefault(async);

        AssertSql(
            """
SELECT SUBSTRING([f].[FirstName], 1, 1) AS [first], SUBSTRING([f].[FirstName], LEN([f].[FirstName]), 1) AS [last]
FROM [FunkyCustomers] AS [f]
ORDER BY [f].[Id]
""");
    }

    public override async Task String_Contains_and_StartsWith_with_same_parameter(bool async)
    {
        await base.String_Contains_and_StartsWith_with_same_parameter(async);

        AssertSql(
            """
@s='B' (Size = 4000)

SELECT [f].[Id], [f].[FirstName], [f].[LastName], [f].[NullableBool]
FROM [FunkyCustomers] AS [f]
WHERE ([f].[FirstName] IS NOT NULL AND (CHARINDEX(@s, [f].[FirstName]) > 0 OR @s LIKE N'')) OR ([f].[LastName] IS NOT NULL AND LEFT([f].[LastName], LEN(@s)) = @s)
""");
    }

    public class FunkyDataQueryAzureSynapseFixture : FunkyDataQueryFixtureBase, ITestSqlLoggerFactory
    {
        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ListLoggerFactory;

        protected override ITestStoreFactory TestStoreFactory
            => AzureSynapseTestStoreFactory.Instance;

        protected override string StoreName
            => nameof(FunkyDataQueryAzureSynapseTest);
    }
}
