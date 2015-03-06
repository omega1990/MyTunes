Use MyTunesDb
GO

SET IDENTITY_INSERT dbo.MP3 ON 

if not exists(select Title from dbo.MP3 where Title = 'Love The Way You Lie')
	INSERT dbo.MP3(MP3ID, Title, Artist, Album, Year) VALUES (1, N'Love The Way You Lie', N'Eminem ft. Rihanna', N'Recovery', 2010)
if not exists(select Title from dbo.MP3 where Title = 'Master of Puppets')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (2, N'Master of Puppets', N'Metallica', N'Master of Puppets', 1989)
if not exists(select Title from dbo.MP3 where Title = 'Jos Jednom')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (3, N'Jos Jednom', N'Silente', N'Lovac na cudesa', 2013)
if not exists(select Title from dbo.MP3 where Title = 'Here Without You')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (4, N'Here Without You', N'3 Doors Down', N'Away From The Sun', 2002)
if not exists(select Title from dbo.MP3 where Title = 'Iris')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (5, N'Iris', N'Goo Goo Dolls', N'Dizzy Up the Girl', 1998)
if not exists(select Title from dbo.MP3 where Title = 'Zombie')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (6, N'Zombie', N'The Cranberries', N'No Need to Argue', 1994)
if not exists(select Title from dbo.MP3 where Title = 'Don''t Speak')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (7, N'Don''t Speak', N'No Doubt', N'Tragic Kingdom', 1995)
if not exists(select Title from dbo.MP3 where Title = 'The Trooper')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (8, N'The Trooper', N'Iron Maiden', N'Piece of Mind', 1983)
if not exists(select Title from dbo.MP3 where Title = 'One')
	INSERT dbo.MP3 (MP3ID, Title, Artist, Album, Year) VALUES (9, N'One', N'U2', N'Achtung Baby', 1991)

SET IDENTITY_INSERT dbo.MP3 OFF