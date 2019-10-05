--GO
SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourCaseClosureReasons] ON 
INSERT [dbo].[AntiSocialBehaviourCaseClosureReasons] ([Id], [Name],[LookupId],IsResolvedReason, [IsActive], [SortOrder]) 
VALUES 

(1,'ABC',1,1,1, 1)
,(2,'Apology Given (Accepted)',1,1,1, 2)
,(3,'Surrender of property',1,0,1, 3)
,(4,'Comp Not Responded',1,0,1,4)
,(5,'Comp Refused Actio',1,0,1, 5)
,(6,'Complaint Withdraw',1,0,1, 6)
,(7,'No Action Possible',1,0,1, 7)
,(8,'No Rep but Continu',1,0,1, 8)
,(9,'Not a L''lord Issue',1,0,1, 9)
,(10,'ASBO',1,1,1, 10)
,(11,'Complaint - Entered In Error',1,1,1, 11)
,(12,'Complaint Not Upheld',1,1,1, 12)
,(13,'Demotion Order',1,1,1, 13)
,(14,'Early Intervention',1,1,1, 14)
,(15,'Error on input',1,1,1, 15)
,(16,'Eviction',1,1,1, 16)
,(17,'Ext of Str/Intro Ten',1,1,1, 17)
,(18,'Indiv Supp Order',1,1,1, 18)
,(19,'Injunction',1,1,1, 19)
,(20,'Joint Visit',1,1,1, 20)
,(21,'Mediation',1,1,1, 21)
,(22,'No Breach of Tenancy',1,1,1, 22)
,(23,'No Reports ASB End',1,1,1, 23)
,(24,'NOSP/Demotion',1,1,1, 24)
,(25,'Other',1,1,1, 25)
,(26,'Parenting Order',1,1,1, 26)
,(27,'Possession Order',1,1,1, 27)
,(28,'Ref to Fam Int Proj',1,1,1, 28)
,(29,'Ref to Parenting Pro',1,1,1, 29)
,(30,'Referral Drug or Alc',1,1,1, 30)
,(31,'Referral MH/Com Care',1,1,1, 31)
,(32,'Referral to Police',1,1,1, 32)
,(33,'Referral to Sup Serv',1,1,1, 33)
,(34,'Referred ASB Panel',1,1,1, 34)
,(35,'T''fer of Comp/Perp',1,1,1, 35)
,(36,'Undertaking to Court',1,1,1, 36)
,(37,'Youth Offending Team',1,1,1, 37)
SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourCaseClosureReasons] OFF
--GO

SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourCaseStatuses] ON 
INSERT [dbo].[AntiSocialBehaviourCaseStatuses] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1,'Open',1,1, 1)
,(2,'InProgress',1,1, 2)
,(3,'OnHold',1,1, 3)
,(4,'Closed',1,1, 3)
SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourCaseStatuses] OFF

SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourTypes] ON 
INSERT [dbo].[AntiSocialBehaviourTypes] ([Id], [Name],[LookupId], [IsActive], [SortOrder]) 
VALUES 
(1,'Drug/substance misuse & dealing -Taking drugs',1,1, 1)
,(2,'Drug/substance misuse & dealing -Sniffing volatile substances',1,1, 2)
,(3,'Drug/substance misuse & dealing -Discarding needles/drug paraphernalia',1,1, 3)
,(4,'Drug/substance misuse & dealing -Crack houses',1,1, 4)
,(5,'Drug/substance misuse & dealing -Presence of dealers or users',1,1, 5)
,(6,'Street drinking',1,1, 6)
,(7,'Begging',1,1, 7)

,(8,'Prostitution - Soliciting',1,1, 8)
,(9,'Prostitution - Cards in phone boxes',1,1,9)
,(10,'Prostitution - Discarded condoms',1,1, 10)

,(11,'Kerb crawling - Loitering',1,1, 11)
,(12,'Kerb crawling - Pestering residents',1,1, 12)

,(13,'Sexual acts : Inappropriate sexual conduct',1,1, 13)
,(14,'Sexual acts : Indecent exposure',1,1, 14)

,(15,'Vehicle-related nuisance - Abandoned cars',1,1, 15)
,(16,'Vehicle-related nuisance - Inconvenient/illegal parking',1,1, 16)
,(17,'Vehicle-related nuisance - Car repairs on the street/in gardens',1,1, 17)
,(18,'Vehicle-related nuisance - Setting vehicles alight',1,1, 18)
,(19,'Vehicle-related nuisance - Joyriding',1,1, 19)
,(20,'Vehicle-related nuisance - Racing cars',1,1, 20)
,(21,'Vehicle-related nuisance - Off-road motorcycling',1,1, 21)
,(22,'Vehicle-related nuisance - Cycling/skateboarding in pedestrian areas/footpaths',1,1, 22)

,(23,'Noise- Noisy neighbours',1,1, 23)
,(24,'Noise- Noisy cars/motorbikes',1,1, 24)
,(25,'Noise- Loud music',1,1, 25)
,(26,'Noise- Alarms (persistent ringing/malfunction)',1,1, 26)
,(27,'Noise- Noise from pubs/clubs',1,1, 27)
,(28,'Noise- Noise from business/industry',1,1, 28)

,(29,'Rowdy behaviour- Shouting & swearing',1,1, 28)
,(30,'Rowdy behaviour- Fighting',1,1, 30)
,(31,'Rowdy behaviour- Drunken behaviour',1,1,31)
,(32,'Rowdy behaviour- Hooliganism/loutish behaviour',1,1, 32)


,(33,'Nuisance behaviour - Urinating in public',1,1,33)
,(34,'Nuisance behaviour - Setting fires (not directed at specific persons or property)',1,1, 34)
,(35,'Nuisance behaviour - Inappropriate use of fireworks',1,1, 35)
,(36,'Nuisance behaviour - Throwing missiles',1,1, 36)
,(37,'Nuisance behaviour - Climbing on buildings',1,1, 37)
,(38,'Nuisance behaviour - Impeding access to communal areas',1,1, 38)
,(39,'Nuisance behaviour - Games in restricted/inappropriate areas',1,1, 39)
,(40,'Nuisance behaviour - Misuse of air guns',1,1, 40)
,(41,'Nuisance behaviour - Letting down tyres',1,1, 41)

,(42,'Hoax calls -False calls to emergency services',1,1, 42)

,(43,'Animal-related problems- Uncontrolled animals',1,1, 43)

,(44,'Intimidation/harassment - Groups or individuals making threats',1,1, 44)
,(45,'Intimidation/harassment -Verbal abuse',1,1, 45)
,(46,'Intimidation/harassment -Bullying',1,1, 46)
,(47,'Intimidation/harassment -Following people',1,1, 47)
,(48,'Intimidation/harassment -Pestering people',1,1, 48)
,(49,'Intimidation/harassment -Voyeurism',1,1, 49)
,(50,'Intimidation/harassment -Sending nasty/offensive letters',1,1, 50)
,(51,'Intimidation/harassment -Obscene/nuisance phone calls',1,1, 51)
,(52,'Menacing gestures on the grounds of- Race',1,1, 52)
,(53,'Menacing gestures on the grounds of - Sexual orientation',1,1, 53)
,(54,'Menacing gestures on the grounds of - Gender',1,1, 54)
,(55,'Menacing gestures on the grounds of - Religion',1,1, 55)
,(56,'Menacing gestures on the grounds of - Disability',1,1, 56)
,(57,'Menacing gestures on the grounds of - Age',1,1, 57)


,(58,'Criminal damage/vandalism - Graffiti',1,1, 58)
,(59,'Criminal damage/vandalism - Damage to bus shelters',1,1, 59)
,(60,'Criminal damage/vandalism - Damage to phone kiosks',1,1, 60)
,(61,'Criminal damage/vandalism - Damage to street furniture',1,1, 61)
,(62,'Criminal damage/vandalism - Damage to buildings',1,1, 62)
,(63,'Criminal damage/vandalism - Damage to trees/plants/hedges',1,1, 63)

,(64,'Litter/rubbish - Dropping litter',1,1, 64)
,(65,'Litter/rubbish - Dumping rubbish',1,1, 65)
,(66,'Litter/rubbish - Fly-tipping',1,1, 66)
,(67,'Litter/rubbish - Fly-posting',1,1, 67)
SET IDENTITY_INSERT [dbo].[AntiSocialBehaviourTypes] OFF