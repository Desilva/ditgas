USE [ditgas_db]
GO
/****** Object:  Table [dbo].[kalendar_group]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kalendar_group](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[group_id] [int] NOT NULL,
	[member] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_kalendar_group] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kalendar_events]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kalendar_events](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[text] [nvarchar](50) NOT NULL,
	[description] [nvarchar](300) NULL,
	[start_date] [datetime] NOT NULL,
	[end_date] [datetime] NOT NULL,
	[type] [nvarchar](10) NULL,
	[priority] [nvarchar](10) NOT NULL,
	[create_by] [int] NULL,
	[group_id] [int] NULL,
 CONSTRAINT [PK_kalendar_events] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dashboard_default]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dashboard_default](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipe] [varchar](20) NULL,
	[konten] [varchar](max) NULL,
 CONSTRAINT [PK_dashboard_home_default] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[company]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[company](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nama] [varchar](255) NOT NULL,
	[parent] [int] NULL,
	[deskripsi] [text] NULL,
 CONSTRAINT [PK_company] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bisnis_main]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bisnis_main](
	[company_id] [int] NOT NULL,
	[profile] [varchar](max) NULL,
	[struktur] [varchar](max) NULL,
	[visimisi] [varchar](max) NULL,
	[ad_art] [varchar](max) NULL,
	[rjpp] [varchar](max) NULL,
	[rkap] [varchar](max) NULL,
 CONSTRAINT [PK_bisnis_main] PRIMARY KEY CLUSTERED 
(
	[company_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bisnis_kpi]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bisnis_kpi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[company_id] [int] NOT NULL,
	[content] [varchar](max) NULL,
	[tahun] [int] NOT NULL,
	[deskripsi] [varchar](max) NULL,
 CONSTRAINT [PK_bisnis_kpi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[bisnis_bussiness_report]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[bisnis_bussiness_report](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[company_id] [int] NOT NULL,
	[content] [varchar](max) NULL,
	[bulan] [varchar](max) NOT NULL,
	[tahun] [int] NOT NULL,
	[deskripsi] [varchar](max) NULL,
 CONSTRAINT [PK_bisnis_bussiness_report] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_jabatan]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_jabatan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nama] [varchar](255) NOT NULL,
	[parent] [int] NULL,
	[deskripsi] [text] NULL,
	[company_id] [int] NULL,
	[golongan] [varchar](50) NULL,
	[cost_centre] [int] NULL,
 CONSTRAINT [PK_users_jabatan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 1) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](255) NULL,
	[fullname] [nvarchar](255) NULL,
	[password] [nvarchar](255) NULL,
	[create_date] [datetime] NULL,
	[rm_role] [int] NULL,
	[no_pekerja] [varchar](50) NULL,
	[lokasi] [varchar](50) NULL,
	[direktorat] [varchar](50) NULL,
	[tempat_lahir] [varchar](50) NULL,
	[tgl_lahir] [date] NULL,
	[jenis_kelamin] [tinyint] NULL,
	[agama] [varchar](50) NULL,
	[susunan_keluarga] [varchar](50) NULL,
	[NPWP] [varchar](50) NULL,
	[alamat_pajak] [varchar](256) NULL,
	[alamat_rumah] [varchar](256) NULL,
	[alamat_ktp] [varchar](256) NULL,
	[tgl_mppk] [date] NULL,
	[tgl_pensiun] [date] NULL,
	[sub_area] [varchar](50) NULL,
	[tgl_aktif] [date] NULL,
	[tmt_dapen_dplk] [date] NULL,
	[tmt_jabatan] [date] NULL,
	[tmt_gol_jabatan] [date] NULL,
	[gol_upah_persero] [varchar](50) NULL,
	[tmt_gol_upah_persero] [date] NULL,
	[gol_upah_ap] [varchar](50) NULL,
	[tmt_gol_upah_ap] [date] NULL,
	[layering] [varchar](50) NULL,
	[pendidikan_terakhir] [varchar](50) NULL,
	[status_pekerja] [varchar](50) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dashboard_text]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dashboard_text](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipe] [varchar](20) NOT NULL,
	[tgl_update] [datetime] NOT NULL,
	[ringkasan] [text] NOT NULL,
	[konten] [text] NOT NULL,
	[poster_user_id] [int] NOT NULL,
	[kategori] [varchar](20) NULL,
 CONSTRAINT [PK_dashboard_home_text] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_tindakan_disiplin]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_tindakan_disiplin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_users_tindakan_disiplin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_sertifikasi]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_sertifikasi](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_users_sertifikasi] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_riwayat_gol_upah_persero]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_riwayat_gol_upah_persero](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[gol_upah_persero] [varchar](50) NOT NULL,
	[tgl_awal] [date] NOT NULL,
	[tgl_akhir] [date] NOT NULL,
 CONSTRAINT [PK_users_riwayat_gol_upah_persero] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_penugasan]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_penugasan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_users_penugasan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_penghargaan]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_penghargaan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_users_penghargaan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_pengalaman_kerja]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_pengalaman_kerja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[nama_perusahaan] [varchar](50) NULL,
	[jabatan] [varchar](50) NULL,
	[lokasi] [varchar](50) NULL,
	[tgl_awal] [date] NULL,
	[tgl_akhir] [date] NULL,
 CONSTRAINT [PK_users_pengalaman_kerja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_pendidikan]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_pendidikan](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[lokasi] [varchar](50) NULL,
	[tingkat] [varchar](50) NULL,
	[tgl_awal] [date] NULL,
	[tgl_akhir] [date] NULL,
 CONSTRAINT [PK_users_pendidikan] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_nilai_kinerja]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users_nilai_kinerja](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
 CONSTRAINT [PK_users_nilai_kinerja] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users_kursus]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_kursus](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[tipe] [varchar](50) NULL,
	[nama] [varchar](50) NULL,
	[lokasi] [varchar](50) NULL,
	[hasil] [varchar](50) NULL,
	[tgl_awal] [date] NULL,
	[tgl_akhir] [date] NULL,
 CONSTRAINT [PK_users_kursus] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users_keluarga]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users_keluarga](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[nama] [varchar](50) NOT NULL,
	[hubungan] [varchar](50) NOT NULL,
	[tempat_lahir] [varchar](50) NULL,
	[tgl_lahir] [date] NULL,
 CONSTRAINT [PK_users_keluarga] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[acl]    Script Date: 08/21/2013 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[acl](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[page_name] [varchar](50) NOT NULL,
	[page_company_id] [int] NULL,
	[allow_view] [tinyint] NOT NULL,
	[allow_create] [tinyint] NOT NULL,
	[allow_update] [tinyint] NOT NULL,
	[allow_delete] [tinyint] NOT NULL,
	[allow_print] [tinyint] NOT NULL,
 CONSTRAINT [PK_acl] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Default [DF_acl_allow_view]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl] ADD  CONSTRAINT [DF_acl_allow_view]  DEFAULT ((0)) FOR [allow_view]
GO
/****** Object:  Default [DF_acl_allow_create]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl] ADD  CONSTRAINT [DF_acl_allow_create]  DEFAULT ((0)) FOR [allow_create]
GO
/****** Object:  Default [DF_acl_allow_update]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl] ADD  CONSTRAINT [DF_acl_allow_update]  DEFAULT ((0)) FOR [allow_update]
GO
/****** Object:  Default [DF_acl_allow_delete]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl] ADD  CONSTRAINT [DF_acl_allow_delete]  DEFAULT ((0)) FOR [allow_delete]
GO
/****** Object:  Default [DF_acl_allow_print]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl] ADD  CONSTRAINT [DF_acl_allow_print]  DEFAULT ((0)) FOR [allow_print]
GO
/****** Object:  Default [DF_dashboard_text_ringkasan]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[dashboard_text] ADD  CONSTRAINT [DF_dashboard_text_ringkasan]  DEFAULT ('') FOR [ringkasan]
GO
/****** Object:  Default [DF__Users__CreateDat__66603565]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF__Users__CreateDat__66603565]  DEFAULT (CONVERT([datetime],CONVERT([varchar],getdate(),(1)),(1))) FOR [create_date]
GO
/****** Object:  Default [DF_Users_rm_role]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_Users_rm_role]  DEFAULT ((0)) FOR [rm_role]
GO
/****** Object:  Default [DF_users_jabatan_company_id]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_jabatan] ADD  CONSTRAINT [DF_users_jabatan_company_id]  DEFAULT ((0)) FOR [company_id]
GO
/****** Object:  ForeignKey [FK_acl_company]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl]  WITH CHECK ADD  CONSTRAINT [FK_acl_company] FOREIGN KEY([page_company_id])
REFERENCES [dbo].[company] ([id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[acl] CHECK CONSTRAINT [FK_acl_company]
GO
/****** Object:  ForeignKey [FK_acl_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[acl]  WITH CHECK ADD  CONSTRAINT [FK_acl_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[acl] CHECK CONSTRAINT [FK_acl_users]
GO
/****** Object:  ForeignKey [FK_dashboard_text_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[dashboard_text]  WITH CHECK ADD  CONSTRAINT [FK_dashboard_text_users] FOREIGN KEY([poster_user_id])
REFERENCES [dbo].[users] ([id])
ON UPDATE CASCADE
GO
ALTER TABLE [dbo].[dashboard_text] CHECK CONSTRAINT [FK_dashboard_text_users]
GO
/****** Object:  ForeignKey [FK_users_users_jabatan]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [FK_users_users_jabatan] FOREIGN KEY([rm_role])
REFERENCES [dbo].[users_jabatan] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [FK_users_users_jabatan]
GO
/****** Object:  ForeignKey [FK_users_jabatan_company]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_jabatan]  WITH CHECK ADD  CONSTRAINT [FK_users_jabatan_company] FOREIGN KEY([company_id])
REFERENCES [dbo].[company] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_jabatan] CHECK CONSTRAINT [FK_users_jabatan_company]
GO
/****** Object:  ForeignKey [FK_users_keluarga_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_keluarga]  WITH CHECK ADD  CONSTRAINT [FK_users_keluarga_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_keluarga] CHECK CONSTRAINT [FK_users_keluarga_users]
GO
/****** Object:  ForeignKey [FK_users_kursus_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_kursus]  WITH CHECK ADD  CONSTRAINT [FK_users_kursus_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_kursus] CHECK CONSTRAINT [FK_users_kursus_users]
GO
/****** Object:  ForeignKey [FK_users_nilai_kinerja_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_nilai_kinerja]  WITH CHECK ADD  CONSTRAINT [FK_users_nilai_kinerja_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_nilai_kinerja] CHECK CONSTRAINT [FK_users_nilai_kinerja_users]
GO
/****** Object:  ForeignKey [FK_users_pendidikan_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_pendidikan]  WITH CHECK ADD  CONSTRAINT [FK_users_pendidikan_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_pendidikan] CHECK CONSTRAINT [FK_users_pendidikan_users]
GO
/****** Object:  ForeignKey [FK_users_pengalaman_kerja_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_pengalaman_kerja]  WITH CHECK ADD  CONSTRAINT [FK_users_pengalaman_kerja_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_pengalaman_kerja] CHECK CONSTRAINT [FK_users_pengalaman_kerja_users]
GO
/****** Object:  ForeignKey [FK_users_penghargaan_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_penghargaan]  WITH CHECK ADD  CONSTRAINT [FK_users_penghargaan_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_penghargaan] CHECK CONSTRAINT [FK_users_penghargaan_users]
GO
/****** Object:  ForeignKey [FK_users_penugasan_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_penugasan]  WITH CHECK ADD  CONSTRAINT [FK_users_penugasan_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_penugasan] CHECK CONSTRAINT [FK_users_penugasan_users]
GO
/****** Object:  ForeignKey [FK_users_riwayat_gol_upah_persero_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_riwayat_gol_upah_persero]  WITH CHECK ADD  CONSTRAINT [FK_users_riwayat_gol_upah_persero_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_riwayat_gol_upah_persero] CHECK CONSTRAINT [FK_users_riwayat_gol_upah_persero_users]
GO
/****** Object:  ForeignKey [FK_users_sertifikasi_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_sertifikasi]  WITH CHECK ADD  CONSTRAINT [FK_users_sertifikasi_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_sertifikasi] CHECK CONSTRAINT [FK_users_sertifikasi_users]
GO
/****** Object:  ForeignKey [FK_users_tindakan_disiplin_users]    Script Date: 08/21/2013 13:06:56 ******/
ALTER TABLE [dbo].[users_tindakan_disiplin]  WITH CHECK ADD  CONSTRAINT [FK_users_tindakan_disiplin_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[users_tindakan_disiplin] CHECK CONSTRAINT [FK_users_tindakan_disiplin_users]
GO
