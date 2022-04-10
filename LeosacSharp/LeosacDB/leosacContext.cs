using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LeosacDB
{
    public partial class leosacContext : DbContext
    {
        public leosacContext()
        {
        }

        public leosacContext(DbContextOptions<leosacContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccessPoint> AccessPoints { get; set; } = null!;
        public virtual DbSet<AccessPointEvent> AccessPointEvents { get; set; } = null!;
        public virtual DbSet<AccessPointUpdate> AccessPointUpdates { get; set; } = null!;
        public virtual DbSet<AccessPointUpdate1> AccessPointUpdates1 { get; set; } = null!;
        public virtual DbSet<AuditEntry> AuditEntries { get; set; } = null!;
        public virtual DbSet<AuditEntryChild> AuditEntryChildren { get; set; } = null!;
        public virtual DbSet<Credential> Credentials { get; set; } = null!;
        public virtual DbSet<CredentialEvent> CredentialEvents { get; set; } = null!;
        public virtual DbSet<Door> Doors { get; set; } = null!;
        public virtual DbSet<DoorEvent> DoorEvents { get; set; } = null!;
        public virtual DbSet<Group> Groups { get; set; } = null!;
        public virtual DbSet<GroupEvent> GroupEvents { get; set; } = null!;
        public virtual DbSet<HardwareBuzzer> HardwareBuzzers { get; set; } = null!;
        public virtual DbSet<HardwareDevice> HardwareDevices { get; set; } = null!;
        public virtual DbSet<HardwareExternalMessage> HardwareExternalMessages { get; set; } = null!;
        public virtual DbSet<HardwareExternalServer> HardwareExternalServers { get; set; } = null!;
        public virtual DbSet<HardwareGpio> HardwareGpios { get; set; } = null!;
        public virtual DbSet<HardwareLed> HardwareLeds { get; set; } = null!;
        public virtual DbSet<HardwareRfidreader> HardwareRfidreaders { get; set; } = null!;
        public virtual DbSet<LogEntry> LogEntries { get; set; } = null!;
        public virtual DbSet<PinCode> PinCodes { get; set; } = null!;
        public virtual DbSet<Rfidcard> Rfidcards { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<ScheduleEvent> ScheduleEvents { get; set; } = null!;
        public virtual DbSet<ScheduleMapping> ScheduleMappings { get; set; } = null!;
        public virtual DbSet<ScheduleMapping1> ScheduleMappings1 { get; set; } = null!;
        public virtual DbSet<ScheduleMappingCred> ScheduleMappingCreds { get; set; } = null!;
        public virtual DbSet<ScheduleMappingDoor> ScheduleMappingDoors { get; set; } = null!;
        public virtual DbSet<ScheduleMappingGroup> ScheduleMappingGroups { get; set; } = null!;
        public virtual DbSet<ScheduleMappingUser> ScheduleMappingUsers { get; set; } = null!;
        public virtual DbSet<ScheduleTimeframe> ScheduleTimeframes { get; set; } = null!;
        public virtual DbSet<SchemaVersion> SchemaVersions { get; set; } = null!;
        public virtual DbSet<SmtpAudit> SmtpAudits { get; set; } = null!;
        public virtual DbSet<SmtpConfig> SmtpConfigs { get; set; } = null!;
        public virtual DbSet<SmtpConfigServer> SmtpConfigServers { get; set; } = null!;
        public virtual DbSet<Token> Tokens { get; set; } = null!;
        public virtual DbSet<Update> Updates { get; set; } = null!;
        public virtual DbSet<UpdateEvent> UpdateEvents { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserEvent> UserEvents { get; set; } = null!;
        public virtual DbSet<UserGroupMembership> UserGroupMemberships { get; set; } = null!;
        public virtual DbSet<UserGroupMembershipEvent> UserGroupMembershipEvents { get; set; } = null!;
        public virtual DbSet<Wsapicall> Wsapicalls { get; set; } = null!;
        public virtual DbSet<Zone> Zones { get; set; } = null!;
        public virtual DbSet<ZoneChild> ZoneChildren { get; set; } = null!;
        public virtual DbSet<ZoneDoor> ZoneDoors { get; set; } = null!;
        public virtual DbSet<ZoneEvent> ZoneEvents { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccessPoint>(entity =>
            {
                entity.ToTable("AccessPoint");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias).HasColumnName("alias");

                entity.Property(e => e.ControllerModule).HasColumnName("controller_module");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<AccessPointEvent>(entity =>
            {
                entity.ToTable("AccessPointEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetApId).HasColumnName("target_ap_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AccessPointEvent)
                    .HasForeignKey<AccessPointEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.AccessPointEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<AccessPointUpdate>(entity =>
            {
                entity.ToTable("AccessPointUpdate");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.AccessPointUpdate)
                    .HasForeignKey<AccessPointUpdate>(d => d.Id)
                    .HasConstraintName("id_fk");
            });

            modelBuilder.Entity<AccessPointUpdate1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AccessPoint_updates");

                entity.HasIndex(e => e.Index, "AccessPoint_updates_index_i");

                entity.HasIndex(e => e.ObjectId, "AccessPoint_updates_object_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Object)
                    .WithMany()
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("object_id_fk");

                entity.HasOne(d => d.ValueNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Value)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_fk");
            });

            modelBuilder.Entity<AuditEntry>(entity =>
            {
                entity.ToTable("AuditEntry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Author).HasColumnName("author");

                entity.Property(e => e.Duration).HasColumnName("duration");

                entity.Property(e => e.EventMask).HasColumnName("event_mask");

                entity.Property(e => e.Finalized).HasColumnName("finalized");

                entity.Property(e => e.Msg).HasColumnName("msg");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.AuditEntries)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("author_fk");
            });

            modelBuilder.Entity<AuditEntryChild>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("AuditEntry_children");

                entity.HasIndex(e => e.Index, "AuditEntry_children_index_i");

                entity.HasIndex(e => e.ObjectId, "AuditEntry_children_object_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Object)
                    .WithMany()
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("object_id_fk");

                entity.HasOne(d => d.ValueNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Value)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_fk");
            });

            modelBuilder.Entity<Credential>(entity =>
            {
                entity.ToTable("Credential");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias).HasColumnName("alias");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.OdbVersion).HasColumnName("odb_version");

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.Property(e => e.ValidityEnabled).HasColumnName("validity_enabled");

                entity.Property(e => e.ValidityEnd)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("validity_end");

                entity.Property(e => e.ValidityStart)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("validity_start");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Credentials)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("owner_fk");
            });

            modelBuilder.Entity<CredentialEvent>(entity =>
            {
                entity.ToTable("CredentialEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetCredId).HasColumnName("target_cred_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.CredentialEvent)
                    .HasForeignKey<CredentialEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.CredentialEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<Door>(entity =>
            {
                entity.ToTable("Door");

                entity.HasIndex(e => e.AccessPoint, "Door_access_point_i")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AccessPoint).HasColumnName("access_point");

                entity.Property(e => e.Alias).HasColumnName("alias");

                entity.Property(e => e.Desc).HasColumnName("desc");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.AccessPointNavigation)
                    .WithOne(p => p.Door)
                    .HasForeignKey<Door>(d => d.AccessPoint)
                    .HasConstraintName("access_point_fk");
            });

            modelBuilder.Entity<DoorEvent>(entity =>
            {
                entity.ToTable("DoorEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AccessPointIdAfter).HasColumnName("access_point_id_after");

                entity.Property(e => e.AccessPointIdBefore).HasColumnName("access_point_id_before");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetDoorId).HasColumnName("target_door_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.DoorEvent)
                    .HasForeignKey<DoorEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.DoorEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("Group");

                entity.HasIndex(e => e.Name, "Group_name_i")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<GroupEvent>(entity =>
            {
                entity.ToTable("GroupEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetGroupId).HasColumnName("target_group_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.GroupEvent)
                    .HasForeignKey<GroupEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.GroupEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<HardwareBuzzer>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_BUZZER_pkey");

                entity.ToTable("HARDWARE_BUZZER");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.DefaultBlinkDuration).HasColumnName("default_blink_duration");

                entity.Property(e => e.DefaultBlinkSpeed).HasColumnName("default_blink_speed");

                entity.Property(e => e.GpioUuid).HasColumnName("gpio_uuid");

                entity.HasOne(d => d.GpioUu)
                    .WithMany(p => p.HardwareBuzzers)
                    .HasForeignKey(d => d.GpioUuid)
                    .HasConstraintName("gpio_uuid_fk");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareBuzzer)
                    .HasForeignKey<HardwareBuzzer>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<HardwareDevice>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_Device_pkey");

                entity.ToTable("HARDWARE_Device");

                entity.HasIndex(e => e.Name, "HARDWARE_Device_name_i")
                    .IsUnique();

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.DeviceClass).HasColumnName("device_class");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<HardwareExternalMessage>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_ExternalMessage_pkey");

                entity.ToTable("HARDWARE_ExternalMessage");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.Payload).HasColumnName("payload");

                entity.Property(e => e.Subject).HasColumnName("subject");

                entity.Property(e => e.Virtualtype).HasColumnName("virtualtype");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareExternalMessage)
                    .HasForeignKey<HardwareExternalMessage>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<HardwareExternalServer>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_ExternalServer_pkey");

                entity.ToTable("HARDWARE_ExternalServer");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.Host).HasColumnName("host");

                entity.Property(e => e.Port).HasColumnName("port");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareExternalServer)
                    .HasForeignKey<HardwareExternalServer>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<HardwareGpio>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_GPIO_pkey");

                entity.ToTable("HARDWARE_GPIO");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.DefaultValue).HasColumnName("default_value");

                entity.Property(e => e.Direction).HasColumnName("direction");

                entity.Property(e => e.Number).HasColumnName("number");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareGpio)
                    .HasForeignKey<HardwareGpio>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<HardwareLed>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_LED_pkey");

                entity.ToTable("HARDWARE_LED");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.Property(e => e.DefaultBlinkDuration).HasColumnName("default_blink_duration");

                entity.Property(e => e.DefaultBlinkSpeed).HasColumnName("default_blink_speed");

                entity.Property(e => e.GpioUuid).HasColumnName("gpio_uuid");

                entity.HasOne(d => d.GpioUu)
                    .WithMany(p => p.HardwareLeds)
                    .HasForeignKey(d => d.GpioUuid)
                    .HasConstraintName("gpio_uuid_fk");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareLed)
                    .HasForeignKey<HardwareLed>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<HardwareRfidreader>(entity =>
            {
                entity.HasKey(e => e.IdUuid)
                    .HasName("HARDWARE_RFIDReader_pkey");

                entity.ToTable("HARDWARE_RFIDReader");

                entity.Property(e => e.IdUuid)
                    .ValueGeneratedNever()
                    .HasColumnName("id_uuid");

                entity.HasOne(d => d.IdUu)
                    .WithOne(p => p.HardwareRfidreader)
                    .HasForeignKey<HardwareRfidreader>(d => d.IdUuid)
                    .HasConstraintName("id_uuid_fk");
            });

            modelBuilder.Entity<LogEntry>(entity =>
            {
                entity.ToTable("LogEntry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Msg).HasColumnName("msg");

                entity.Property(e => e.RunId).HasColumnName("run_id");

                entity.Property(e => e.ThreadId).HasColumnName("thread_id");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<PinCode>(entity =>
            {
                entity.ToTable("PinCode");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.PinCode1).HasColumnName("pin_code");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.PinCode)
                    .HasForeignKey<PinCode>(d => d.Id)
                    .HasConstraintName("id_fk");
            });

            modelBuilder.Entity<Rfidcard>(entity =>
            {
                entity.ToTable("RFIDCard");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.NbBits).HasColumnName("nb_bits");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Rfidcard)
                    .HasForeignKey<Rfidcard>(d => d.Id)
                    .HasConstraintName("id_fk");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.HasIndex(e => e.Name, "Schedule_name_i")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.OdbVersion).HasColumnName("odb_version");
            });

            modelBuilder.Entity<ScheduleEvent>(entity =>
            {
                entity.ToTable("ScheduleEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetSchedId).HasColumnName("target_sched_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ScheduleEvent)
                    .HasForeignKey<ScheduleEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.ScheduleEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<ScheduleMapping>(entity =>
            {
                entity.ToTable("ScheduleMapping");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias).HasColumnName("alias");

                entity.Property(e => e.OdbVersion).HasColumnName("odb_version");
            });

            modelBuilder.Entity<ScheduleMapping1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Schedule_mapping");

                entity.HasIndex(e => e.Index, "Schedule_mapping_index_i");

                entity.HasIndex(e => e.ScheduleId, "Schedule_mapping_schedule_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.ScheduleMappingId).HasColumnName("schedule_mapping_id");

                entity.HasOne(d => d.Schedule)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("schedule_id_fk");

                entity.HasOne(d => d.ScheduleMapping)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleMappingId)
                    .HasConstraintName("schedule_mapping_id_fk");
            });

            modelBuilder.Entity<ScheduleMappingCred>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScheduleMapping_creds");

                entity.HasIndex(e => e.ScheduleMappingId, "ScheduleMapping_creds_schedule_mapping_id_i");

                entity.Property(e => e.CredentialId).HasColumnName("credential_id");

                entity.Property(e => e.ScheduleMappingId).HasColumnName("schedule_mapping_id");

                entity.HasOne(d => d.Credential)
                    .WithMany()
                    .HasForeignKey(d => d.CredentialId)
                    .HasConstraintName("credential_id_fk");

                entity.HasOne(d => d.ScheduleMapping)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleMappingId)
                    .HasConstraintName("schedule_mapping_id_fk");
            });

            modelBuilder.Entity<ScheduleMappingDoor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScheduleMapping_doors");

                entity.HasIndex(e => e.ScheduleMappingId, "ScheduleMapping_doors_schedule_mapping_id_i");

                entity.Property(e => e.DoorId).HasColumnName("door_id");

                entity.Property(e => e.ScheduleMappingId).HasColumnName("schedule_mapping_id");

                entity.HasOne(d => d.Door)
                    .WithMany()
                    .HasForeignKey(d => d.DoorId)
                    .HasConstraintName("door_id_fk");

                entity.HasOne(d => d.ScheduleMapping)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleMappingId)
                    .HasConstraintName("schedule_mapping_id_fk");
            });

            modelBuilder.Entity<ScheduleMappingGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScheduleMapping_groups");

                entity.HasIndex(e => e.ScheduleMappingId, "ScheduleMapping_groups_schedule_mapping_id_i");

                entity.Property(e => e.GroupId).HasColumnName("group_id");

                entity.Property(e => e.ScheduleMappingId).HasColumnName("schedule_mapping_id");

                entity.HasOne(d => d.Group)
                    .WithMany()
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("group_id_fk");

                entity.HasOne(d => d.ScheduleMapping)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleMappingId)
                    .HasConstraintName("schedule_mapping_id_fk");
            });

            modelBuilder.Entity<ScheduleMappingUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ScheduleMapping_users");

                entity.HasIndex(e => e.ScheduleMappingId, "ScheduleMapping_users_schedule_mapping_id_i");

                entity.Property(e => e.ScheduleMappingId).HasColumnName("schedule_mapping_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.ScheduleMapping)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleMappingId)
                    .HasConstraintName("schedule_mapping_id_fk");

                entity.HasOne(d => d.User)
                    .WithMany()
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("user_id_fk");
            });

            modelBuilder.Entity<ScheduleTimeframe>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Schedule_timeframes");

                entity.HasIndex(e => e.Index, "Schedule_timeframes_index_i");

                entity.HasIndex(e => e.ScheduleId, "Schedule_timeframes_schedule_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.TimeframeDay).HasColumnName("timeframe_day");

                entity.Property(e => e.TimeframeEndHour).HasColumnName("timeframe_end_hour");

                entity.Property(e => e.TimeframeEndMin).HasColumnName("timeframe_end_min");

                entity.Property(e => e.TimeframeStartHour).HasColumnName("timeframe_start_hour");

                entity.Property(e => e.TimeframeStartMin).HasColumnName("timeframe_start_min");

                entity.HasOne(d => d.Schedule)
                    .WithMany()
                    .HasForeignKey(d => d.ScheduleId)
                    .HasConstraintName("schedule_id_fk");
            });

            modelBuilder.Entity<SchemaVersion>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("schema_version_pkey");

                entity.ToTable("schema_version");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Migration).HasColumnName("migration");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<SmtpAudit>(entity =>
            {
                entity.ToTable("SMTP_Audit");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.SmtpAudit)
                    .HasForeignKey<SmtpAudit>(d => d.Id)
                    .HasConstraintName("id_fk");
            });

            modelBuilder.Entity<SmtpConfig>(entity =>
            {
                entity.ToTable("SMTP_Config");

                entity.Property(e => e.Id).HasColumnName("id");
            });

            modelBuilder.Entity<SmtpConfigServer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("SMTP_Config_servers");

                entity.HasIndex(e => e.Index, "SMTP_Config_servers_index_i");

                entity.HasIndex(e => e.SmtpconfigId, "SMTP_Config_servers_smtpconfig_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.SmtpconfigId).HasColumnName("smtpconfig_id");

                entity.Property(e => e.ValueCaInfoFile).HasColumnName("value_CA_info_file");

                entity.Property(e => e.ValueEnabled).HasColumnName("value_enabled");

                entity.Property(e => e.ValueFrom).HasColumnName("value_from");

                entity.Property(e => e.ValueMsTimeout).HasColumnName("value_ms_timeout");

                entity.Property(e => e.ValuePassword).HasColumnName("value_password");

                entity.Property(e => e.ValueUrl).HasColumnName("value_url");

                entity.Property(e => e.ValueUsername).HasColumnName("value_username");

                entity.Property(e => e.ValueVerifyHost).HasColumnName("value_verify_host");

                entity.Property(e => e.ValueVerifyPeer).HasColumnName("value_verify_peer");

                entity.HasOne(d => d.Smtpconfig)
                    .WithMany()
                    .HasForeignKey(d => d.SmtpconfigId)
                    .HasConstraintName("smtpconfig_id_fk");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.HasKey(e => e.Token1)
                    .HasName("Token_pkey");

                entity.ToTable("Token");

                entity.Property(e => e.Token1)
                    .HasMaxLength(128)
                    .HasColumnName("token");

                entity.Property(e => e.Expiration)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("expiration");

                entity.Property(e => e.Owner).HasColumnName("owner");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.OwnerNavigation)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.Owner)
                    .HasConstraintName("owner_fk");
            });

            modelBuilder.Entity<Update>(entity =>
            {
                entity.ToTable("Update");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CheckpointLast).HasColumnName("checkpoint_last");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.GeneratedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("generated_at");

                entity.Property(e => e.OdbVersion).HasColumnName("odb_version");

                entity.Property(e => e.SourceModule).HasColumnName("source_module");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusUpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("status_updated_at");

                entity.Property(e => e.Typeid).HasColumnName("typeid");

                entity.HasOne(d => d.CheckpointLastNavigation)
                    .WithMany(p => p.Updates)
                    .HasForeignKey(d => d.CheckpointLast)
                    .HasConstraintName("checkpoint_last_fk");
            });

            modelBuilder.Entity<UpdateEvent>(entity =>
            {
                entity.ToTable("UpdateEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UpdateEvent)
                    .HasForeignKey<UpdateEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.UpdateEvents)
                    .HasForeignKey(d => d.Target)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Username, "User_username_i")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email).HasColumnName("email");

                entity.Property(e => e.Firstname).HasColumnName("firstname");

                entity.Property(e => e.Lastname).HasColumnName("lastname");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .HasColumnName("username");

                entity.Property(e => e.ValidityEnabled).HasColumnName("validity_enabled");

                entity.Property(e => e.ValidityEnd)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("validity_end");

                entity.Property(e => e.ValidityStart)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("validity_start");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<UserEvent>(entity =>
            {
                entity.ToTable("UserEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserEvent)
                    .HasForeignKey<UserEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.UserEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("target_fk");
            });

            modelBuilder.Entity<UserGroupMembership>(entity =>
            {
                entity.ToTable("UserGroupMembership");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Group).HasColumnName("group");

                entity.Property(e => e.Rank).HasColumnName("rank");

                entity.Property(e => e.Timestamp)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("timestamp");

                entity.Property(e => e.User).HasColumnName("user");

                entity.Property(e => e.Version).HasColumnName("version");

                entity.HasOne(d => d.GroupNavigation)
                    .WithMany(p => p.UserGroupMemberships)
                    .HasForeignKey(d => d.Group)
                    .HasConstraintName("group_fk");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.UserGroupMemberships)
                    .HasForeignKey(d => d.User)
                    .HasConstraintName("user_fk");
            });

            modelBuilder.Entity<UserGroupMembershipEvent>(entity =>
            {
                entity.ToTable("UserGroupMembershipEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.TargetGroup).HasColumnName("target_group");

                entity.Property(e => e.TargetGroupId).HasColumnName("target_group_id");

                entity.Property(e => e.TargetUser).HasColumnName("target_user");

                entity.Property(e => e.TargetUserId).HasColumnName("target_user_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UserGroupMembershipEvent)
                    .HasForeignKey<UserGroupMembershipEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetGroupNavigation)
                    .WithMany(p => p.UserGroupMembershipEvents)
                    .HasForeignKey(d => d.TargetGroup)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_group_fk");

                entity.HasOne(d => d.TargetUserNavigation)
                    .WithMany(p => p.UserGroupMembershipEvents)
                    .HasForeignKey(d => d.TargetUser)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_user_fk");
            });

            modelBuilder.Entity<Wsapicall>(entity =>
            {
                entity.ToTable("WSAPICall");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.ApiMethod).HasColumnName("api_method");

                entity.Property(e => e.DatabaseOperations).HasColumnName("database_operations");

                entity.Property(e => e.RequestContent).HasColumnName("request_content");

                entity.Property(e => e.ResponseContent).HasColumnName("response_content");

                entity.Property(e => e.SourceEndpoint).HasColumnName("source_endpoint");

                entity.Property(e => e.StatusCode).HasColumnName("status_code");

                entity.Property(e => e.StatusString).HasColumnName("status_string");

                entity.Property(e => e.Uuid).HasColumnName("uuid");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Wsapicall)
                    .HasForeignKey<Wsapicall>(d => d.Id)
                    .HasConstraintName("id_fk");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.ToTable("Zone");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Alias).HasColumnName("alias");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Type).HasColumnName("type");

                entity.Property(e => e.Version).HasColumnName("version");
            });

            modelBuilder.Entity<ZoneChild>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Zone_children");

                entity.HasIndex(e => e.Index, "Zone_children_index_i");

                entity.HasIndex(e => e.ObjectId, "Zone_children_object_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Object)
                    .WithMany()
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("object_id_fk");

                entity.HasOne(d => d.ValueNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Value)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_fk");
            });

            modelBuilder.Entity<ZoneDoor>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Zone_doors");

                entity.HasIndex(e => e.Index, "Zone_doors_index_i");

                entity.HasIndex(e => e.ObjectId, "Zone_doors_object_id_i");

                entity.Property(e => e.Index).HasColumnName("index");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Value).HasColumnName("value");

                entity.HasOne(d => d.Object)
                    .WithMany()
                    .HasForeignKey(d => d.ObjectId)
                    .HasConstraintName("object_id_fk");

                entity.HasOne(d => d.ValueNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Value)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("value_fk");
            });

            modelBuilder.Entity<ZoneEvent>(entity =>
            {
                entity.ToTable("ZoneEvent");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.After).HasColumnName("after");

                entity.Property(e => e.Before).HasColumnName("before");

                entity.Property(e => e.Target).HasColumnName("target");

                entity.Property(e => e.TargetZoneId).HasColumnName("target_zone_id");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ZoneEvent)
                    .HasForeignKey<ZoneEvent>(d => d.Id)
                    .HasConstraintName("id_fk");

                entity.HasOne(d => d.TargetNavigation)
                    .WithMany(p => p.ZoneEvents)
                    .HasForeignKey(d => d.Target)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("target_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
