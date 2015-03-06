USE MyTunesDb
GO

SET IDENTITY_INSERT dbo.Playlist ON 

if not exists(select Name from dbo.Playlist where Name = 'Fun')
	INSERT dbo.Playlist (PlaylistID, Name) VALUES (1, N'Fun')
if not exists(select Name from dbo.Playlist where Name = 'Sunday')
	INSERT dbo.Playlist (PlaylistID, Name) VALUES (2, N'Sunday')
if not exists(select Name from dbo.Playlist where Name = 'Home')
	INSERT dbo.Playlist (PlaylistID, Name) VALUES (3, N'Home')
if not exists(select Name from dbo.Playlist where Name = 'Work')
	INSERT dbo.Playlist (PlaylistID, Name) VALUES (4, N'Work')

SET IDENTITY_INSERT dbo.Playlist OFF
