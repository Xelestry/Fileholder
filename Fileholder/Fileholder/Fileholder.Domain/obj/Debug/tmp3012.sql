CREATE TABLE [dbo].[UserFiles] (
    [file_id] [nvarchar](128) NOT NULL,
    [file_name] [nvarchar](max),
    [file_extension] [nvarchar](max),
    [file_guid_folder] [nvarchar](max),
    [user_id] [nvarchar](max),
    [file_size] [nvarchar](max),
    [file_upload_time] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.UserFiles] PRIMARY KEY ([file_id])
)
CREATE TABLE [dbo].[__MigrationHistory] (
    [MigrationId] [nvarchar](150) NOT NULL,
    [ContextKey] [nvarchar](300) NOT NULL,
    [Model] [varbinary](max) NOT NULL,
    [ProductVersion] [nvarchar](32) NOT NULL,
    CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY ([MigrationId], [ContextKey])
)
INSERT [dbo].[__MigrationHistory]([MigrationId], [ContextKey], [Model], [ProductVersion])
VALUES (N'201810150440333_FirstMigration', N'Fileholder.Domain.Migrations.Configuration',  0x1F8B0800000000000400CD58CD6EDB3810BE17D8771074EA02A998A4976E20B748EDB8085A274595F41AD0D248269622B52415D8FB6A7BD847DA57D8A1FE2DF937F5160B5FA4E1CC373F1C7E1CF99FBFFEF63F2C53EE3C83D24C8A917BE19DBB0E8850464C24233737F19B77EE87F7BFBCF26FA274E97CAFF5DE5A3DB4147AE42E8CC9AE08D1E10252AABD94854A6A191B2F9429A1912497E7E7BF918B0B0208E12296E3F8DF7261580AC50BBE8EA508213339E5331901D7951C578202D5B9A329E88C863072A78CC342F2089437912965C2B3D60A0CB8CE356714230A80C7AE438590861A8CF7EA51436094144990A180F2875506A81753AEA1CAE3AA553F34A5F34B9B12690D6BA830D706433B0EF0E26D5523D2377F51A5DDA68658C51BACB659D9AC8B4A8E5C2C88B285D4AED3F77635E6CA6A6EAA7481C3407B8DFD9933D03A6BDA04BBC9FECE9C71CE4DAE602420378AF233E76B3EE72CFC0CAB07F93B8891C839EF068C21E3DA9A00455F95CC4099D53788AB34ACEFDBC875C8BA2DE91B37A63DBB324F6C0DEC76D799D1E517108959E039B87CE762664B886A49D52B8F82E1E14023A3727CBDC3C0E99C43B34EF6FAB54F3B3CE3E3419EF73BBA591A10BAE89F9FE0ED53CEA269D106FFB93BDB7C3B77EF745905ECCF9FB3598F1997347A606D6F4CA881F27D6793F9A43DDCC3238FD468F04882AA5CDD4C27732B83A5D970F2B1B0D5E1D755E4EB7197980198218BB44194F4EB75D63685DB04D6523D29B9BEBE13C8964BC19FD12CC38DE85C1295C409CA1B62FC26389E32D31283847A037336D1369E8C543481DE2ABAC648A74C6983BB47E7D46ED6384A076ADD6DD852E2DA53BFD27D526C0B5F5BD8E7B6B1365F95DE56C0B69C53CC3005618A64A1096C4730857510524ED516C21D4B9EA7A294C5287B6203F6DE0753F2E7004814E2E3A03A0C39C08376ED38D02E110E50135C7C8AABD5C3716BC6EBE2E5287B41F94A461B04A60BF171505DCE1A00E6C5E293619BB7C527BD2EEBF735193476EF8EEF9F945D24D35769BC3764D32315BF3AE0FBC7D1C1892F555C070BF6CC227BDA839536907A56C10BFEE063CE30DF566146058B419B7214727112BCEC4DB2FF9FA992681DF14347CB170C745B3861FF44D71896579078A62A5C50351CEA8E9CD83ADCB28EFC3AA5CB5FBB7087DCF47D663915E41AAD9C00B4E1965305A83B73D48FA3AD714B091AE1B4644E3D2D0D6FF393CC43254360CC7389E197B1FEF8B834642C9F743FB3FD096896B410F6A35B4068A9A005AD756E452CEBCA637EDD886A95DEC6CCC050DC047AAD0C8B6968703904AD8B89F93BE5B91D3ED33944B7E23E37596EAEB58674CE57DD7C7DB2DB7F3113AEC7ECDF67F64D9F22050C93D93EBA171F73C6A326EEE9863EDA02613BE613A0BCA058FC6240B864D520DD4971205055BE09642022BC2E1E20CD3882E97B11D06778496CD8625F20A1E1AABE78B683ECDF88F5B2FB13461345535D61B4F6F6AF2362FF3B7AFF2F2C909E7C6D120000 , N'6.2.0-61023')

