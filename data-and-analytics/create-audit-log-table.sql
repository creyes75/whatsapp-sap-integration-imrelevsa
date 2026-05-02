/* 
=============================================================================
DATABASE OBJECT: Traceability & Audit Log Table
PROJECT: SAP Business One - WhatsApp Integration
AUTHOR: Carlos Reyes
DESCRIPTION: 
This table is the backbone of the system's observability. It captures 
the complete lifecycle of every API request, allowing for real-time 
troubleshooting, performance bottleneck identification, and 
financial audit trail validation.

KEY FEATURES:
- ExecutionId: Cross-system GUID for distributed tracing.
- Payloads: Full JSON storage for historical transaction replay.
- Error Logging: Specific ErrorCode mapping for proactive alerting.
=============================================================================
*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ApiExecutionLog]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ApiExecutionLog](
        [LogId] [int] IDENTITY(1,1) NOT NULL,
        [ExecutionId] [uniqueidentifier] NOT NULL,
        [ApiName] [nvarchar](100) NOT NULL,
        [Endpoint] [nvarchar](255) NOT NULL,
        [HttpMethod] [nvarchar](10) NOT NULL,
        [RequestPayload] [nvarchar](max) NULL,
        [ResponsePayload] [nvarchar](max) NULL,
        [ErrorMessage] [nvarchar](max) NULL,
        [ErrorCode] [nvarchar](25) NULL,
        [CreatedAt] [datetime] NOT NULL DEFAULT (getdate()),
        
        CONSTRAINT [PK_ApiExecutionLog] PRIMARY KEY CLUSTERED ([LogId] ASC)
        WITH (
            PAD_INDEX = OFF, 
            STATISTICS_NORECOMPUTE = OFF, 
            IGNORE_DUP_KEY = OFF, 
            ALLOW_ROW_LOCKS = ON, 
            ALLOW_PAGE_LOCKS = ON, 
            OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF
        ) ON [PRIMARY]
    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO

-- Index for performance optimization during troubleshooting
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_ApiExecutionLog_ExecutionId' AND object_id = OBJECT_ID('[dbo].[ApiExecutionLog]'))
BEGIN
    CREATE INDEX [IX_ApiExecutionLog_ExecutionId] ON [dbo].[ApiExecutionLog] ([ExecutionId]);
END
GO
