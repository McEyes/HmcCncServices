using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc
{

    using System;

    public static class EZNcErr
    {
        // 错误标志
        public const uint ME_ERR_FLG = 0x80000000;
        public const uint ME_GROUP_MASK = 0xFFF0000;
        public const uint ME_CORD_MASK = 0xFFFF;
        public const uint ME_CMD_MASK = 0xFF00;
        public const uint ME_COMMON_MASK = 0x00FF;

        // 公共代码（所有命令）
        public const uint ME_ARG1 = 0x81;
        public const uint ME_ARG2 = ME_ARG1 + 0x1;
        public const uint ME_ARG3 = ME_ARG1 + 0x2;
        public const uint ME_ARG4 = ME_ARG1 + 0x3;
        public const uint ME_ARG5 = ME_ARG1 + 0x4;
        public const uint ME_ARG6 = ME_ARG1 + 0x5;
        public const uint ME_ARG7 = ME_ARG1 + 0x6;
        public const uint ME_ARG8 = ME_ARG1 + 0x7;
        public const uint ME_ARG9 = ME_ARG1 + 0x8;
        public const uint ME_ARG10 = ME_ARG1 + 0x9;
        public const uint ME_ARG11 = ME_ARG1 + 0xA;
        public const uint ME_ARG12 = ME_ARG1 + 0xB;
        public const uint ME_ARG13 = ME_ARG1 + 0xC;
        public const uint ME_ARG14 = ME_ARG1 + 0xD;
        public const uint ME_ARG15 = ME_ARG1 + 0xE;

        // 公共代码（一般相关）
        public const uint ME_COMMON_SYNTAXERR = 0x40;
        public const uint ME_COMMON_CONTROLLER = 0x41;
        public const uint ME_COMMON_NOFILE = 0x42;
        public const uint ME_COMMON_FILESYSTEM = 0x43;
        public const uint ME_COMMON_CMDFORMAT = 0x44;
        public const uint ME_COMMON_SAMPLINGACTIVE = 0x45;
        public const uint ME_COMMON_PLCRUN = 0x46;
        public const uint ME_COMMON_FILEBUSY = 0x47;
        public const uint ME_COMMON_STRINGLENGTH = 0x48;
        public const uint ME_COMMON_NOTSUPPORTED = 0x49;
        public const uint ME_COMMON_PROTECT = 0x4A;
        public const uint ME_COMMON_PGLKC = 0x4B;
        public const uint ME_COMMON_EXECUTING = 0x4C;
        public const uint ME_COMMON_SAFETYPWLOCK = 0x4D;
        public const uint ME_COMMON_RESET = 0x4E;
        public const uint ME_COMMON_PROTECT2 = 0x4F;
        public const uint ME_COMMON_PROTECT3 = 0x50;
        public const uint ME_COMMON_RUNNING = 0x51;
        public const uint ME_COMMON_OCCUPIED = 0x52;

        // 文件相关的公共代码
        public const uint ME_FILE_BUSY = ME_COMMON_FILEBUSY;
        public const uint ME_FILE_NOFILE = ME_COMMON_NOFILE;
        public const uint ME_FILE_NAMELENGTH = ME_COMMON_STRINGLENGTH;
        public const uint ME_FILE_ENTRYOVER = 0x03;
        public const uint ME_FILE_MEMORYOVER = 0x04;
        public const uint ME_FILE_NOTOPEN = 0x90;
        public const uint ME_FILE_NODIR = 0x91;
        public const uint ME_FILE_OPEN = 0x92;
        public const uint ME_FILE_CREATE = 0x93;
        public const uint ME_FILE_READERR = 0x94;
        public const uint ME_FILE_WRITEERR = 0x95;
        public const uint ME_FILE_WRITEDATAOVER = 0x96;
        public const uint ME_FILE_WRITEDATANAME = 0x97;
        public const uint ME_FILE_ILLEGALNAME = 0x98;
        public const uint ME_FILE_FILEFULL = 0x99;
        public const uint ME_FILE_ALREADYEXIST = 0x9A;
        public const uint ME_FILE_NODRIVE = 0x9B;
        public const uint ME_FILE_UOPEN_FORMAT = 0x9C;
        public const uint ME_FILE_ILLEGALFORMAT = 0x9D;
        public const uint ME_FILE_WRONGPASSWORD = 0x9E;
        public const uint ME_FILE_SORT = 0x9F;
        public const uint ME_FILE_SAFE_NOPASSWD = 0xB0;
        public const uint ME_FILE_SAFE_DATAERR = 0xB1;
        public const uint ME_FILE_WRITE_PROTECT = 0xB2;
        public const uint ME_FILE_SAFE_PASSWDERR = 0xB3;
        public const uint ME_FILE_SAFE_CHECKERR = 0xB4;
        public const uint ME_FILE_SEC_DISCREPANCY = 0xB5;
        public const uint ME_FILE_SEC_INVALID = 0xB6;
        public const uint ME_PCFILE_NOTOPEN = 0xA0;
        public const uint ME_PCFILE_NOFILE = 0xA1;
        public const uint ME_PCFILE_NODIR = 0xA2;
        public const uint ME_PCFILE_OPEN = 0xA3;
        public const uint ME_PCFILE_CREATE = 0xA4;
        public const uint ME_PCFILE_READERR = 0xA5;
        public const uint ME_PCFILE_WRITEERR = 0xA6;
        public const uint ME_PCFILE_ILLEGALNAME = 0xA7;
        public const uint ME_PCFILE_NODRIVE = 0xA8;
        public const uint ME_PCFILE_NAMELENGTH = 0xA9;

        // 数据相关的公共代码
        public const uint ME_DATA_ADDR = 0x90;
        public const uint ME_DATA_SECTION = 0x91;
        public const uint ME_DATA_SUBSECTION = 0x92;
        public const uint ME_DATA_ADDR2 = 0x93;
        public const uint ME_DATA_SECTION2 = 0x94;
        public const uint ME_DATA_SUBSECTION2 = 0x95;
        public const uint ME_DATA_SIZE = 0x96;
        public const uint ME_DATA_DATATYPE = 0x97;
        public const uint ME_DATA_SIZE2 = 0x98;
        public const uint ME_DATA_DATATYPE2 = 0x99;
        public const uint ME_DATA_VALUE = 0x9A;
        public const uint ME_DATA_READONLY = 0x9B;
        public const uint ME_DATA_TABLEFULL = 0x9C;
        public const uint ME_DATA_READERR = 0x9D;
        public const uint ME_DATA_WRITEERR = 0x9E;
        public const uint ME_DATA_WRITEONLY = 0x9F;
        public const uint ME_DATA_AXIS = 0xA0;
        public const uint ME_DATA_DATANUM = 0xA1;
        public const uint ME_DATA_UOPEN_FORMAT = 0xA2;
        public const uint ME_DATA_NODATA = 0xA3;
        public const uint ME_DATA_REGIST = 0xA4;
        public const uint ME_DATA_RELEASE = 0xA5;
        public const uint ME_DATA_SAFE_PASSWDERR = 0xA6;
        public const uint ME_DATA_SAFE_CHECKERR = 0xA7;
        public const uint ME_DATA_SORT = 0xA8;
        public const uint ME_DATA_DATATYPE3 = 0xA9;

        // 操作相关的公共代码
        public const uint ME_OPE_NOFILE = ME_COMMON_NOFILE;
        public const uint ME_OPE_FILEBUSY = ME_COMMON_FILEBUSY;
        public const uint ME_OPE_NAMELENGTH = ME_COMMON_STRINGLENGTH;
        public const uint ME_OPE_ADDR = 0x90;
        public const uint ME_OPE_MODE = 0x91;
        public const uint ME_OPE_VALUE = 0x92;
        public const uint ME_OPE_DATASIZE = 0x93;
        public const uint ME_OPE_DATATYPE = 0x94;
        public const uint ME_OPE_LOCALMODE = 0x95;
        public const uint ME_OPE_EXTERNAL = 0x96;
        public const uint ME_OPE_PARAM = 0x97;
        public const uint ME_OPE_RESETACTIVE = 0x98;
        public const uint ME_OPE_EMG = 0x99;
        public const uint ME_OPE_RESETWAIT = 0x9A;
        public const uint ME_OPE_OPERATINGAXIS = 0x9B;
        public const uint ME_OPE_ENCDATAFAIL = 0x9C;
        public const uint ME_OPE_ENCDATAREADFAIL = 0x9D;
        public const uint ME_OPE_SETUPOFF = 0x9E;
        public const uint ME_OPE_OPERATING = 0x9F;
        public const uint ME_OPE_SAMPLINGACTIVE = ME_COMMON_SAMPLINGACTIVE;
        public const uint ME_OPE_NOTSAMPLING = 0xA1;
        public const uint ME_OPE_PLCRUN = ME_COMMON_PLCRUN;
        public const uint ME_OPE_AMPALARM = 0xA3;
        public const uint ME_OPE_AMPNOTEXIST = 0xA4;
        public const uint ME_OPE_AMPRDYOFF = 0xA5;
        public const uint ME_OPE_AMPSERVOOFF = 0xA6;
        public const uint ME_OPE_AMPTYPEERR = 0xA7;
        public const uint ME_OPE_DIRECTMODE = 0xA8;
        public const uint ME_OPE_NOTDIRECTMODE = 0xA9;
        public const uint ME_OPE_DIRECTMODEBUFFULL = 0xAA;
        public const uint ME_OPE_ABSSYSTEM = 0xAB;
        public const uint ME_OPE_NOTABSSYSTEM = 0xAC;
        public const uint ME_OPE_AXISMOVING = 0xAD;
        public const uint ME_OPE_NOTPASSZ = 0xAE;

        // 操作源相关的公共代码
        public const uint ME_OPESRC_PRGFORMAT = 0x1;
        public const uint ME_OPESRC_NOPRG = 0x2;
        public const uint ME_OPESRC_RUNNING = 0x3;
        public const uint ME_OPESRC_RESET = 0x4;
        public const uint ME_OPESRC_LONGPATH = 0x5;
        public const uint ME_OPESRC_NCPCCOM = 0x6;
        public const uint ME_OPESRC_TIMEOUT = 0x7;
        public const uint ME_OPESRC_NBNOTFOUND = 0x8;
        public const uint ME_OPESRC_TOPSEARCH = 0x9;
        public const uint ME_OPESRC_SEARCHING = 0xA;
        public const uint ME_OPESRC_ALREADYSEARCHED = 0xB;
        public const uint ME_OPESRC_OTHERSEARCHING = 0xC;
        public const uint ME_OPESRC_CHECKING = 0xD;
        public const uint ME_OPESRC_ILLEGALPOS = 0xE;
        public const uint ME_OPESRC_REVERSE = 0xF;
        public const uint ME_OPESRC_SORT = 0x10;

        // 设备相关的公共代码
        public const uint ME_DEV_ALREADYOPEN = 1;
        public const uint ME_DEV_NOTOPEN = 2;
        public const uint ME_DEV_CARDNOTEXIST = 4;
        public const uint ME_DEV_BADCHANNEL = 6;
        public const uint ME_DEV_BADFD = 7;
        public const uint ME_DEV_CANNOTOPEN = 8;
        public const uint ME_DEV_NOTCONNECT = 10;
        public const uint ME_DEV_NOTCLOSE = 11;
        public const uint ME_DEV_TIMEOUT = 20;
        public const uint ME_DEV_DATAERR = 21;
        public const uint ME_DEV_CANCELED = 22;
        public const uint ME_DEV_ILLEGALSIZE = 23;
        public const uint ME_DEV_TASKQUIT = 24;
        public const uint ME_DEV_HARDWAREERR = 25;
        public const uint ME_DEV_SYSTEMDOWN = 26;
        public const uint ME_DEV_UNKNOWNFUNC = 50;
        public const uint ME_DEV_SETDATAERR = 51;

        // 命令相关的公共代码
        public const uint ME_CMD = 0x10000;
        public const uint ME_CMD_ERR = (ME_ERR_FLG | ME_CMD); // 命令错误

        public const uint ME_CMD_NOOPTION = (ME_CMD_ERR | 0x1); // 无选项
        public const uint ME_CMD_NOCOMMAND = (ME_CMD_ERR | 0x2); // 无命令
        public const uint ME_CMD_SYNTAX = (ME_CMD_ERR | 0x3); // 语法错误
        public const uint ME_CMD_DIFFER = (ME_CMD_ERR | 0x4); // 命令不同
        public const uint ME_CMD_ERRNUMGETFAIL = (ME_CMD_ERR | 0x5); // 获取命令错误号失败

        // 系统功能相关的公共代码
        public const uint ME_SYSFUNC = 0x20000;
        public const uint ME_SYSFUNC_ERR = (ME_ERR_FLG | ME_SYSFUNC); // 系统功能错误

        public const uint ME_SYSFUNC_IOCTL = (ME_SYSFUNC_ERR | 0x100);
        public const uint ME_SYSFUNC_IOCTL_ADDR = (ME_SYSFUNC_IOCTL | ME_DATA_ADDR); // IO控制地址
        public const uint ME_SYSFUNC_IOCTL_NOTOPEN = (ME_SYSFUNC_IOCTL | ME_DEV_NOTOPEN); // 设备未打开
        public const uint ME_SYSFUNC_IOCTL_FUNCTION = (ME_SYSFUNC_IOCTL | ME_DEV_UNKNOWNFUNC); // 命令未定义
        public const uint ME_SYSFUNC_IOCTL_DATA = (ME_SYSFUNC_IOCTL | ME_DEV_SETDATAERR); // 数据错误
        public const uint ME_SYSFUNC_IOCTL_DISCONNECT = (ME_SYSFUNC_IOCTL | ME_DEV_NOTCONNECT); // 断开连接

        // 文件相关的公共代码
        public const uint ME_FILE = 0x30000;
        public const uint ME_FILE_ERR = (ME_ERR_FLG | ME_FILE); // 文件错误

        public const uint ME_FILE_DIR = (ME_FILE_ERR | 0x100);
        public const uint ME_FILE_DIR_ALREADYOPENED = (ME_FILE_DIR | 0x1); // 目录已打开
        public const uint ME_FILE_DIR_NODIR = (ME_FILE_DIR | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_DIR_NOTOPEN = (ME_FILE_DIR | ME_FILE_NOTOPEN); // 目录未打开
        public const uint ME_FILE_DIR_READ = (ME_FILE_DIR | ME_FILE_READERR); // 文件读取错误
        public const uint ME_FILE_DIR_DATASIZE = (ME_FILE_DIR | 0x3); // 数据大小错误
        public const uint ME_FILE_DIR_NODRIVE = (ME_FILE_DIR | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_DIR_FILESYSTEM = (ME_FILE_DIR | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_DIR_NAMELENGTH = (ME_FILE_DIR | ME_FILE_NAMELENGTH); // 目录名称长度错误
        public const uint ME_FILE_DIR_ILLEGALNAME = (ME_FILE_DIR | ME_FILE_ILLEGALNAME); // 目录名称非法
        public const uint ME_FILE_DIR_WRITE_PROTECT = (ME_FILE_DIR | ME_FILE_WRITE_PROTECT); // 写保护

        public const uint ME_PCFILE_DIR_ALREADYOPENED = (ME_FILE_DIR | 0x2); // PC目录已打开
        public const uint ME_PCFILE_DIR_NODIR = (ME_FILE_DIR | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_DIR_NOFILE = (ME_FILE_DIR | ME_COMMON_NOFILE); // PC无文件
        public const uint ME_PCFILE_DIR_NOTOPEN = (ME_FILE_DIR | ME_PCFILE_NOTOPEN); // PC目录未打开
        public const uint ME_PCFILE_DIR_READ = (ME_FILE_DIR | ME_PCFILE_READERR); // PC文件读取错误
        public const uint ME_PCFILE_DIR_NODRIVE = (ME_FILE_DIR | ME_PCFILE_NODRIVE); // PC无驱动器

        public const uint ME_FILE_DELETE = (ME_FILE_ERR | 0x200);
        public const uint ME_FILE_DEL_NOTDELETE = (ME_FILE_DELETE | 0x1); // 无法删除文件
        public const uint ME_FILE_DEL_NODIR = (ME_FILE_DELETE | ME_FILE_NODIR); // 无此目录
        public const uint ME_FILE_DEL_NOFILE = (ME_FILE_DELETE | ME_COMMON_NOFILE); // 无文件
        public const uint ME_FILE_DEL_ILLEGALNAME = (ME_FILE_DELETE | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_DEL_BUSY = (ME_FILE_DELETE | ME_COMMON_FILEBUSY); // 文件忙
        public const uint ME_FILE_DEL_NODRIVE = (ME_FILE_DELETE | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_DEL_FILESYSTEM = (ME_FILE_DELETE | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_DEL_NAMELENGTH = (ME_FILE_DELETE | ME_FILE_NAMELENGTH); // 文件名称长度错误
        public const uint ME_FILE_DEL_PROTECT = (ME_FILE_DELETE | ME_COMMON_PROTECT); // 文件保护
        public const uint ME_FILE_DEL_WRITE_PROTECT = (ME_FILE_DELETE | ME_FILE_WRITE_PROTECT); // 文件写保护

        public const uint ME_PCFILE_DEL_NOTDELETE = (ME_FILE_DELETE | 0x2); // PC无法删除文件
        public const uint ME_PCFILE_DEL_NODIR = (ME_FILE_DELETE | ME_PCFILE_NODIR); // PC无此目录
        public const uint ME_PCFILE_DEL_NOFILE = (ME_FILE_DELETE | ME_PCFILE_NOFILE); // PC无文件
        public const uint ME_PCFILE_DEL_ILLEGALNAME = (ME_FILE_DELETE | ME_PCFILE_ILLEGALNAME); // PC文件名称非法
        public const uint ME_PCFILE_DEL_NODRIVE = (ME_FILE_DELETE | ME_PCFILE_NODRIVE); // PC无驱动器

        public const uint ME_FILE_RENAME = (ME_FILE_ERR | 0x300);
        public const uint ME_FILE_REN_FILEEXIST = (ME_FILE_RENAME | 0x1); // 文件已存在
        public const uint ME_FILE_REN_NODIR = (ME_FILE_RENAME | ME_FILE_NODIR); // 无此目录
        public const uint ME_FILE_REN_NOFILE = (ME_FILE_RENAME | ME_COMMON_NOFILE); // 无文件
        public const uint ME_FILE_REN_ILLEGALNAME = (ME_FILE_RENAME | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_REN_BUSY = (ME_FILE_RENAME | ME_COMMON_FILEBUSY); // 文件忙
        public const uint ME_FILE_REN_NODRIVE = (ME_FILE_RENAME | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_REN_FILESYSTEM = (ME_FILE_RENAME | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_REN_NOTRENAME = (ME_FILE_RENAME | 0x3); // 无法重命名文件
        public const uint ME_FILE_REN_SAMENAME = (ME_FILE_RENAME | 0x5); // 文件名称相同
        public const uint ME_FILE_REN_NAMELENGTH = (ME_FILE_RENAME | ME_FILE_NAMELENGTH); // 文件名称长度错误
        public const uint ME_FILE_REN_PROTECT = (ME_FILE_RENAME | ME_COMMON_PROTECT); // 文件保护
        public const uint ME_PCFILE_REN_FILEEXIST = (ME_FILE_RENAME | 0x2); // PC文件已存在
        public const uint ME_PCFILE_REN_NODIR = (ME_FILE_RENAME | ME_PCFILE_NODIR); // PC无此目录
        public const uint ME_PCFILE_REN_NOFILE = (ME_FILE_RENAME | ME_PCFILE_NOFILE); // PC无文件
        public const uint ME_PCFILE_REN_ILLEGALNAME = (ME_FILE_RENAME | ME_PCFILE_ILLEGALNAME); // PC文件名称非法
        public const uint ME_PCFILE_REN_NOTRENAME = (ME_FILE_RENAME | 0x4); // PC无法重命名文件
        public const uint ME_PCFILE_REN_SAMENAME = (ME_FILE_RENAME | 0x6); // PC文件名称相同
        public const uint ME_PCFILE_REN_NODRIVE = (ME_FILE_RENAME | ME_PCFILE_NODRIVE); // PC无驱动器


        // 文件复制相关的公共代码
        public const uint ME_FILE_COPY = (ME_FILE_ERR | 0x400); // 文件复制错误
        public const uint ME_FILE_COPY_FILEEXIST = (ME_FILE_COPY | 0x1); // 文件已存在
        public const uint ME_FILE_COPY_NODIR = (ME_FILE_COPY | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_COPY_NOFILE = (ME_FILE_COPY | ME_COMMON_NOFILE); // 无文件
        public const uint ME_FILE_COPY_ILLEGALNAME = (ME_FILE_COPY | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_COPY_READ = (ME_FILE_COPY | ME_FILE_READERR); // 文件读取错误
        public const uint ME_FILE_COPY_WRITE = (ME_FILE_COPY | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_FILE_COPY_BUSY = (ME_FILE_COPY | ME_COMMON_FILEBUSY); // 文件忙
        public const uint ME_FILE_COPY_SAMPLING = (ME_FILE_COPY | ME_COMMON_SAMPLINGACTIVE); // 采样活动
        public const uint ME_FILE_COPY_PLCRUN = (ME_FILE_COPY | ME_COMMON_PLCRUN); // PLC运行中
        public const uint ME_FILE_COPY_NODRIVE = (ME_FILE_COPY | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_COPY_FILESYSTEM = (ME_FILE_COPY | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_COPY_ENTRYOVER = (ME_FILE_COPY | 0x3); // 条目溢出
        public const uint ME_FILE_COPY_MEMORYOVER = (ME_FILE_COPY | 0x4); // 内存溢出
        public const uint ME_FILE_COPY_NAMELENGTH = (ME_FILE_COPY | ME_FILE_NAMELENGTH); // 文件名称长度错误
        public const uint ME_FILE_COPY_PROTECT = (ME_FILE_COPY | ME_COMMON_PROTECT); // 文件保护
        public const uint ME_FILE_COPY_WRITE_WARNING = (ME_FILE_COPY | ME_FILE_WRITEERR); // 写入警告
        public const uint ME_FILE_COPY_DIFFER = (ME_FILE_COPY | 0x5); // 差异
        public const uint ME_FILE_COPY_NOTSUPPORTED = (ME_FILE_COPY | ME_COMMON_NOTSUPPORTED); // 不支持的操作
        public const uint ME_FILE_COPY_NOTOPEN = (ME_FILE_COPY | ME_FILE_NOTOPEN); // 文件未打开
        public const uint ME_FILE_COPY_EXECUTING = (ME_FILE_COPY | ME_COMMON_EXECUTING); // 正在执行
        public const uint ME_FILE_COPY_SAFETYPWLOCK = (ME_FILE_COPY | ME_COMMON_SAFETYPWLOCK); // 安全密码锁定
        public const uint ME_FILE_COPY_ILLEGALFORMAT = (ME_FILE_COPY | ME_FILE_ILLEGALFORMAT); // 文件格式非法
        public const uint ME_FILE_COPY_WRONGPASSWORD = (ME_FILE_COPY | ME_FILE_WRONGPASSWORD); // 密码错误
        public const uint ME_FILE_COPY_WRITE_PROTECT = (ME_FILE_COPY | ME_FILE_WRITE_PROTECT); // 写保护
        public const uint ME_FILE_COPY_SAFE_DATAERR = (ME_FILE_COPY | ME_FILE_SAFE_DATAERR); // 安全数据错误
        public const uint ME_FILE_COPY_SAFE_NOPASSWD = (ME_FILE_COPY | ME_FILE_SAFE_NOPASSWD); // 无密码
        public const uint ME_FILE_COPY_SAFE_PASSWDERR = (ME_FILE_COPY | ME_FILE_SAFE_PASSWDERR); // 安全密码错误
        public const uint ME_FILE_COPY_SAFE_CHECKERR = (ME_FILE_COPY | ME_FILE_SAFE_CHECKERR); // 安全检查错误
        public const uint ME_FILE_COPY_SORT = (ME_FILE_COPY | ME_FILE_SORT); // 文件排序

        // PC文件复制相关的公共代码
        public const uint ME_PCFILE_COPY_FILEEXIST = (ME_FILE_COPY | 0x2); // PC文件已存在
        public const uint ME_PCFILE_COPY_NODIR = (ME_FILE_COPY | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_COPY_NOFILE = (ME_FILE_COPY | ME_PCFILE_NOFILE); // PC无文件
        public const uint ME_PCFILE_COPY_ILLEGALNAME = (ME_FILE_COPY | ME_PCFILE_ILLEGALNAME); // PC文件名称非法
        public const uint ME_PCFILE_COPY_OPEN = (ME_FILE_COPY | ME_PCFILE_OPEN); // PC文件已打开
        public const uint ME_PCFILE_COPY_CREATE = (ME_FILE_COPY | ME_PCFILE_CREATE); // PC文件创建
        public const uint ME_PCFILE_COPY_READ = (ME_FILE_COPY | ME_PCFILE_READERR); // PC文件读取错误
        public const uint ME_PCFILE_COPY_WRITE = (ME_FILE_COPY | ME_PCFILE_WRITEERR); // PC文件写入错误
        public const uint ME_PCFILE_COPY_NODRIVE = (ME_FILE_COPY | ME_PCFILE_NODRIVE); // PC无驱动器
        public const uint ME_PCFILE_COPY_NOTOPEN = (ME_FILE_COPY | ME_PCFILE_NOTOPEN); // PC文件未打开
        public const uint ME_PCFILE_COPY_MEMORYOVER = (ME_FILE_COPY | 0x6); // PC内存溢出

        // 文件压缩相关的公共代码
        public const uint ME_FILE_CONDENSE = (ME_FILE_ERR | 0x500); // 文件压缩错误
        public const uint ME_FILE_COND_NOFILE = (ME_FILE_CONDENSE | ME_COMMON_NOFILE); // 无文件
        public const uint ME_FILE_COND_READ = (ME_FILE_CONDENSE | ME_FILE_READERR); // 读取错误
        public const uint ME_FILE_COND_WRITE = (ME_FILE_CONDENSE | ME_FILE_WRITEERR); // 写入错误
        public const uint ME_FILE_COND_NODRIVE = (ME_FILE_CONDENSE | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_COND_FILESYSTEM = (ME_FILE_CONDENSE | ME_COMMON_FILESYSTEM); // 文件系统错误

        // 磁盘空间相关的公共代码
        public const uint ME_FILE_DISKFREE = (ME_FILE_ERR | 0x600); // 磁盘空间错误
        public const uint ME_FILE_DISKFREE_NODIR = (ME_FILE_DISKFREE | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_DISKFREE_NODRIVE = (ME_FILE_DISKFREE | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_DISKFREE_FILESYSTEM = (ME_FILE_DISKFREE | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_DISKFREE_NAMELENGTH = (ME_FILE_DISKFREE | ME_FILE_NAMELENGTH); // 文件名称长度错误
        public const uint ME_FILE_DISKFREE_ILLEGALNAME = (ME_FILE_DISKFREE | ME_FILE_NAMELENGTH); // 文件名称非法
        public const uint ME_PCFILE_DISKFREE_NODIR = (ME_FILE_DISKFREE | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_DISKFREE_NODRIVE = (ME_FILE_DISKFREE | ME_PCFILE_NODRIVE); // PC无驱动器

        // 驱动器列表相关的公共代码
        public const uint ME_FILE_DRIVELIST = (ME_FILE_ERR | 0x700); // 驱动器列表错误
        public const uint ME_FILE_DRVLIST_DATASIZE = (ME_FILE_DRIVELIST | 0x1); // 驱动器数据大小
        public const uint ME_FILE_DRVLIST_READ = (ME_FILE_DRIVELIST | ME_FILE_READERR); // 驱动器读取错误

        // 文件验证相关的公共代码
        public const uint ME_FILE_VERIFY = (ME_FILE_ERR | 0x800); // 文件验证错误
        public const uint ME_FILE_VERIFY_NODIR = (ME_FILE_VERIFY | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_VERIFY_NOFILE = (ME_FILE_VERIFY | ME_COMMON_NOFILE); // 无文件
        public const uint ME_FILE_VERIFY_ILLEGALNAME = (ME_FILE_VERIFY | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_VERIFY_READ = (ME_FILE_VERIFY | ME_FILE_READERR); // 读取错误
        public const uint ME_FILE_VERIFY_NODRIVE = (ME_FILE_VERIFY | ME_FILE_NODRIVE); // 无驱动器
        public const uint ME_FILE_VERIFY_FILESYSTEM = (ME_FILE_VERIFY | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_VERIFY_NAMELENGTH = (ME_FILE_VERIFY | ME_FILE_NAMELENGTH); // 文件名称长度错误
        public const uint ME_FILE_VERIFY_DIFFER = (ME_FILE_VERIFY | 0x5); // 差异
        public const uint ME_FILE_VERIFY_NOTSUPPORTED = (ME_FILE_VERIFY | ME_COMMON_NOTSUPPORTED); // 不支持的操作
        public const uint ME_PCFILE_VERIFY_NODIR = (ME_FILE_VERIFY | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_VERIFY_NOFILE = (ME_FILE_VERIFY | ME_PCFILE_NOFILE); // PC无文件
        public const uint ME_PCFILE_VERIFY_OPEN = (ME_FILE_VERIFY | ME_PCFILE_OPEN); // PC文件已打开
        public const uint ME_PCFILE_VERIFY_NODRIVE = (ME_FILE_VERIFY | ME_PCFILE_NODRIVE); // PC无驱动器

        // 注册信息相关的公共代码
        public const uint ME_FILE_REGISTINFO = (ME_FILE_ERR | 0x900); // 注册信息错误
        public const uint ME_FILE_REGISTINFO_ILLEGALNAME = (ME_FILE_REGISTINFO | ME_FILE_ILLEGALNAME); // 注册信息名称非法
        public const uint ME_FILE_REGISTINFO_ILLIGALINDEX = (ME_FILE_REGISTINFO | 0x1); // 注册信息索引非法
        public const uint ME_FILE_REGISTINFO_ILLEGALCOMMENT = (ME_FILE_REGISTINFO | 0x2); // 注册信息注释非法
        public const uint ME_FILE_REGISTINFO_MEMADDR = (ME_FILE_REGISTINFO | 0x3); // 注册信息内存地址

        // 创建目录相关的公共代码
        public const uint ME_FILE_CREATEDIR = (ME_FILE_ERR | 0xA00); // 创建目录错误
        public const uint ME_FILE_CREATEDIR_FILESYSTEM = (ME_FILE_CREATEDIR | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_CREATEDIR_ILLEGALNAME = (ME_FILE_CREATEDIR | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_CREATEDIR_NAMELENGTH = (ME_FILE_CREATEDIR | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FILE_CREATEDIR_NODIR = (ME_FILE_CREATEDIR | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_CREATEDIR_WRITEERR = (ME_FILE_CREATEDIR | ME_FILE_WRITEERR); // 写入错误
        public const uint ME_FILE_CREATEDIR_NOTSUPPORTED = (ME_FILE_CREATEDIR | ME_COMMON_NOTSUPPORTED); // 不支持的操作
        public const uint ME_FILE_CREATEDIR_ALREADYOPENED = (ME_FILE_CREATEDIR | 0x1); // 目录已打开
        public const uint ME_FILE_CREATEDIR_FILEEXIST = (ME_FILE_CREATEDIR | 0x3); // 文件已存在
        public const uint ME_FILE_CREATEDIR_MEMORYOVER = (ME_FILE_CREATEDIR | 0x5); // 内存溢出
        public const uint ME_FILE_CREATEDIR_ROOTDIRFULL = (ME_FILE_CREATEDIR | 0x7); // 根目录已满
        public const uint ME_FILE_CREATEDIR_WRITE_PROTECT = (ME_FILE_CREATEDIR | ME_FILE_WRITE_PROTECT); // 写保护
        public const uint ME_PCFILE_CREATEDIR_ILLEGALNAME = (ME_FILE_CREATEDIR | ME_PCFILE_ILLEGALNAME); // PC文件名称非法
        public const uint ME_PCFILE_CREATEDIR_NAMELENGTH = (ME_FILE_CREATEDIR | ME_PCFILE_NAMELENGTH); // PC名称长度错误
        public const uint ME_PCFILE_CREATEDIR_NODIR = (ME_FILE_CREATEDIR | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_CREATEDIR_WRITEERR = (ME_FILE_CREATEDIR | ME_PCFILE_WRITEERR); // PC写入错误
        public const uint ME_PCFILE_CREATEDIR_ALREADYOPENED = (ME_FILE_CREATEDIR | 0x2); // PC目录已打开
        public const uint ME_PCFILE_CREATEDIR_FILEEXIST = (ME_FILE_CREATEDIR | 0x4); // PC文件已存在
        public const uint ME_PCFILE_CREATEDIR_MEMORYOVER = (ME_FILE_CREATEDIR | 0x6); // PC内存溢出
        public const uint ME_PCFILE_CREATEDIR_ROOTDIRFULL = (ME_FILE_CREATEDIR | 0x8); // PC根目录已满

        // 删除目录相关的公共代码
        public const uint ME_FILE_DELETEDIR = (ME_FILE_ERR | 0xB00); // 删除目录错误
        public const uint ME_FILE_DELETEDIR_FILESYSTEM = (ME_FILE_DELETEDIR | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_FILE_DELETEDIR_ILLEGALNAME = (ME_FILE_DELETEDIR | ME_FILE_ILLEGALNAME); // 文件名称非法
        public const uint ME_FILE_DELETEDIR_NAMELENGTH = (ME_FILE_DELETEDIR | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FILE_DELETEDIR_NODIR = (ME_FILE_DELETEDIR | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_DELETEDIR_NOTSUPPORTED = (ME_FILE_DELETEDIR | ME_COMMON_NOTSUPPORTED); // 不支持的操作
        public const uint ME_FILE_DELETEDIR_ALREADYOPENED = (ME_FILE_DELETEDIR | 0x1); // 目录已打开
        public const uint ME_FILE_DELETEDIR_NOTEMPTY = (ME_FILE_DELETEDIR | 0x3); // 目录非空
        public const uint ME_FILE_DELETEDIR_NOTDELETE = (ME_FILE_DELETEDIR | 0x5); // 删除失败
        public const uint ME_FILE_DELETEDIR_WRITE_PROTECT = (ME_FILE_DELETEDIR | ME_FILE_WRITE_PROTECT); // 写保护
        public const uint ME_PCFILE_DELETEDIR_ILLEGALNAME = (ME_FILE_DELETEDIR | ME_PCFILE_ILLEGALNAME); // PC文件名称非法
        public const uint ME_PCFILE_DELETEDIR_NAMELENGTH = (ME_FILE_DELETEDIR | ME_PCFILE_NAMELENGTH); // PC名称长度错误
        public const uint ME_PCFILE_DELETEDIR_NODIR = (ME_FILE_DELETEDIR | ME_PCFILE_NODIR); // PC无目录
        public const uint ME_PCFILE_DELETEDIR_ALREADYOPENED = (ME_FILE_DELETEDIR | 0x2); // PC目录已打开
        public const uint ME_PCFILE_DELETEDIR_NOTEMPTY = (ME_FILE_DELETEDIR | 0x4); // PC目录非空
        public const uint ME_PCFILE_DELETEDIR_NOTDELETE = (ME_FILE_DELETEDIR | 0x6); // PC删除失败

        // 复制目录相关的公共代码
        public const uint ME_FILE_COPYDIR = (ME_FILE_ERR | 0xC00); // 复制目录错误
        public const uint ME_FILE_COPYDIR_NAMELENGTH = (ME_FILE_COPYDIR | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FILE_COPYDIR_NODIR = (ME_FILE_COPYDIR | ME_FILE_NODIR); // 无目录
        public const uint ME_FILE_COPYDIR_NOTCOPY = (ME_FILE_COPYDIR | 0x6); // 未复制

        // 背光相关的公共代码
        public const uint ME_BKLIGHT = (ME_FILE_ERR | 0xD00); // 背光错误
        public const uint ME_BKLIGHT_NOTSUPPORTED = (ME_BKLIGHT | ME_COMMON_NOTSUPPORTED); // 不支持的操作
        public const uint ME_BKLIGHT_DATA_VALUE = (ME_BKLIGHT | ME_DATA_VALUE); // 背光数据值

        // 数据相关的公共代码
        public const uint ME_DATA = 0x40000; // 数据
        public const uint ME_DATA_ERR = (ME_ERR_FLG | ME_DATA); // 数据错误

        // 数据读取相关的公共代码
        public const uint ME_DATA_READ = (ME_DATA_ERR | 0x100); // 数据读取错误
        public const uint ME_DATA_READ_ADDR = (ME_DATA_READ | ME_DATA_ADDR); // 地址读取错误
        public const uint ME_DATA_READ_SECT = (ME_DATA_READ | ME_DATA_SECTION); // 节读取错误
        public const uint ME_DATA_READ_SUBSECT = (ME_DATA_READ | ME_DATA_SUBSECTION); // 子节读取错误
        public const uint ME_DATA_READ_AXIS = (ME_DATA_READ | ME_DATA_AXIS); // 轴读取错误
        public const uint ME_DATA_READ_DATASIZE = (ME_DATA_READ | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_DATA_READ_DATATYPE = (ME_DATA_READ | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_DATA_READ_WRITEONLY = (ME_DATA_READ | ME_DATA_WRITEONLY); // 只读错误
        public const uint ME_DATA_READ_READ = (ME_DATA_READ | ME_DATA_READERR); // 读取错误
        public const uint ME_DATA_READ_DATANUM = (ME_DATA_READ | ME_DATA_DATANUM); // 数据数量错误
        public const uint ME_DATA_READ_NODATA = (ME_DATA_READ | ME_DATA_NODATA); // 无数据错误
        public const uint ME_DATA_READ_VALUE = (ME_DATA_READ | ME_DATA_VALUE); // 数据值读取错误

        // 数据写入相关的公共代码
        public const uint ME_DATA_WRITE = (ME_DATA_ERR | 0x200); // 数据写入错误
        public const uint ME_DATA_WRITE_ADDR = (ME_DATA_WRITE | ME_DATA_ADDR); // 地址写入错误
        public const uint ME_DATA_WRITE_SECT = (ME_DATA_WRITE | ME_DATA_SECTION); // 节写入错误
        public const uint ME_DATA_WRITE_SUBSECT = (ME_DATA_WRITE | ME_DATA_SUBSECTION); // 子节写入错误
        public const uint ME_DATA_WRITE_AXIS = (ME_DATA_WRITE | ME_DATA_AXIS); // 轴写入错误
        public const uint ME_DATA_WRITE_DATASIZE = (ME_DATA_WRITE | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_DATA_WRITE_DATATYPE = (ME_DATA_WRITE | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_DATA_WRITE_READONLY = (ME_DATA_WRITE | ME_DATA_READONLY); // 只写错误
        public const uint ME_DATA_WRITE_WRITE = (ME_DATA_WRITE | ME_DATA_WRITEERR); // 写入错误
        public const uint ME_DATA_WRITE_SAFETYPWLOCK = (ME_DATA_WRITE | ME_COMMON_SAFETYPWLOCK); // 安全密码锁定错误
        public const uint ME_DATA_WRITE_UOPEN_FORMAT = (ME_DATA_WRITE | ME_DATA_UOPEN_FORMAT); // SRAM格式错误
        public const uint ME_DATA_WRITE_EDTFILE_REGIST = (ME_DATA_WRITE | ME_DATA_REGIST); // 编辑文件注册错误
        public const uint ME_DATA_WRITE_EDTFILE_RELEASE = (ME_DATA_WRITE | ME_DATA_RELEASE); // 编辑文件释放错误
        public const uint ME_DATA_WRITE_NODATA = (ME_DATA_WRITE | ME_DATA_NODATA); // 无数据错误
        public const uint ME_DATA_WRITE_VALUE = (ME_DATA_WRITE | ME_DATA_VALUE); // 数据值写入错误
        public const uint ME_DATA_WRITE_SAFE_NOPASSWD = (ME_DATA_WRITE | ME_DATA_SAFE_PASSWDERR); // 安全无密码错误
        public const uint ME_DATA_WRITE_SAFE_CHECKERR = (ME_DATA_WRITE | ME_DATA_SAFE_CHECKERR); // 安全检查错误
        public const uint ME_DATA_WRITE_SAFE_DATATYPE = (ME_DATA_WRITE | ME_DATA_DATATYPE3); // 安全数据类型错误
        public const uint ME_DATA_WRITE_SORT = (ME_DATA_WRITE | ME_DATA_SORT); // 数据排序错误
        public const uint ME_DATA_WRITE_ULPROTECT_CHK = (ME_DATA_WRITE | ME_COMMON_PROTECT2); // 上层保护检查错误
        public const uint ME_DATA_WRITE_PROTECT = (ME_DATA_WRITE | ME_COMMON_PROTECT); // 数据保护错误
        public const uint ME_DATA_WRITE_ULPROTECT_PART_CHK = (ME_DATA_WRITE | ME_COMMON_PROTECT3); // 上层部分保护检查错误
        public const uint ME_DATA_WRITE_RUNNING = (ME_DATA_WRITE | ME_COMMON_RUNNING); // 正在运行错误
        public const uint ME_DATA_WRITE_OCCUPIED = (ME_DATA_WRITE | ME_COMMON_OCCUPIED); // 被占用错误


        // 数据复制相关的公共代码
        public const uint ME_DATA_COPY = (ME_DATA_ERR | 0x300); // 数据复制错误
        public const uint ME_DATA_COPY_SRCADDR = (ME_DATA_COPY | ME_DATA_ADDR); // 源地址错误
        public const uint ME_DATA_COPY_SRCSECT = (ME_DATA_COPY | ME_DATA_SECTION); // 源节错误
        public const uint ME_DATA_COPY_SRCSUBSECT = (ME_DATA_COPY | ME_DATA_SUBSECTION); // 源子节错误
        public const uint ME_DATA_COPY_DSTADDR = (ME_DATA_COPY | ME_DATA_ADDR2); // 目的地址错误
        public const uint ME_DATA_COPY_DSTSECT = (ME_DATA_COPY | ME_DATA_SECTION2); // 目的节错误
        public const uint ME_DATA_COPY_DSTSUBSECT = (ME_DATA_COPY | ME_DATA_SUBSECTION2); // 目的子节错误
        public const uint ME_DATA_COPY_READ = (ME_DATA_COPY | ME_DATA_READERR); // 读取错误
        public const uint ME_DATA_COPY_WRITE = (ME_DATA_COPY | ME_DATA_WRITEERR); // 写入错误

        // 模块注册相关的公共代码
        public const uint ME_DATA_MDLREGIST = (ME_DATA_ERR | 0x400); // 模块注册错误
        public const uint ME_DATA_MDLREGIST_ADDR = (ME_DATA_MDLREGIST | ME_DATA_ADDR); // 地址注册错误
        public const uint ME_DATA_MDLREGIST_SECT = (ME_DATA_MDLREGIST | ME_DATA_SECTION); // 节注册错误
        public const uint ME_DATA_MDLREGIST_SUBSECT = (ME_DATA_MDLREGIST | ME_DATA_SUBSECTION); // 子节注册错误
        public const uint ME_DATA_MDLREGIST_AXIS = (ME_DATA_MDLREGIST | ME_DATA_AXIS); // 轴注册错误
        public const uint ME_DATA_MDLREGIST_REGIST = (ME_DATA_MDLREGIST | 0x1); // 注册错误
        public const uint ME_DATA_MDLREGIST_PRIORITY = (ME_DATA_MDLREGIST | 0x2); // 优先级错误
        public const uint ME_DATA_MDLREGIST_WRITEONLY = (ME_DATA_MDLREGIST | ME_DATA_WRITEONLY); // 只写错误
        public const uint ME_DATA_MDLREGIST_READONLY = (ME_DATA_MDLREGIST | ME_DATA_READONLY); // 只读错误
        public const uint ME_DATA_MDLREGIST_DATATYPE = (ME_DATA_MDLREGIST | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_DATA_MDLREGIST_READ = (ME_DATA_MDLREGIST | ME_DATA_READERR); // 读取错误

        // 模块取消相关的公共代码
        public const uint ME_DATA_MDLCANCEL = (ME_DATA_ERR | 0x500); // 模块取消错误
        public const uint ME_DATA_MDLCANCEL_ADDR = (ME_DATA_MDLCANCEL | ME_DATA_ADDR); // 地址取消错误
        public const uint ME_DATA_MDLCANCEL_NOTREGIST = (ME_DATA_MDLCANCEL | 0x1); // 未注册错误

        // 模块读取相关的公共代码
        public const uint ME_DATA_MDLREAD = (ME_DATA_ERR | 0x600); // 模块读取错误
        public const uint ME_DATA_MDLREAD_ADDR = (ME_DATA_MDLREAD | ME_DATA_ADDR); // 地址读取错误
        public const uint ME_DATA_MDLREAD_NOTREGIST = (ME_DATA_MDLREAD | 0x1); // 未注册错误
        public const uint ME_DATA_MDLREAD_DATASIZE = (ME_DATA_MDLREAD | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_DATA_MDLREAD_DATATYPE = (ME_DATA_MDLREAD | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_DATA_MDLREAD_WRITEONLY = (ME_DATA_MDLREAD | ME_DATA_WRITEONLY); // 只写错误
        public const uint ME_DATA_MDLREAD_READ = (ME_DATA_MDLREAD | ME_DATA_READERR); // 读取错误

        // 数据注册相关的公共代码
        public const uint ME_DATA_CLCTREGIST = (ME_DATA_ERR | 0x700); // 数据注册错误
        public const uint ME_DATA_CLCTREGIST_ADDR = (ME_DATA_CLCTREGIST | ME_DATA_ADDR); // 地址注册错误
        public const uint ME_DATA_CLCTREGIST_SECT = (ME_DATA_CLCTREGIST | ME_DATA_SECTION); // 节注册错误
        public const uint ME_DATA_CLCTREGIST_SUBSECT = (ME_DATA_CLCTREGIST | ME_DATA_SUBSECTION); // 子节注册错误
        public const uint ME_DATA_CLCTREGIST_TABLEFULL = (ME_DATA_CLCTREGIST | ME_DATA_TABLEFULL); // 表满错误
        public const uint ME_DATA_CLCTREGIST_REGIST = (ME_DATA_CLCTREGIST | 0x1); // 注册错误

        // 数据取消相关的公共代码
        public const uint ME_DATA_CLCTCANCEL = (ME_DATA_ERR | 0x800); // 数据取消错误
        public const uint ME_DATA_CLCTCANCEL_ADDR = (ME_DATA_CLCTCANCEL | ME_DATA_ADDR); // 地址取消错误
        public const uint ME_DATA_CLCTCANCEL_ID = (ME_DATA_CLCTCANCEL | 0x1); // ID取消错误

        // 数据读取相关的公共代码
        public const uint ME_DATA_CLCTREAD = (ME_DATA_ERR | 0x900); // 数据读取错误
        public const uint ME_DATA_CLCTREAD_ADDR = (ME_DATA_CLCTREAD | ME_DATA_ADDR); // 地址读取错误
        public const uint ME_DATA_CLCTREAD_DATATYPE = (ME_DATA_CLCTREAD | ME_DATA_DATATYPE); // 数据类型错误

        // 模块写入相关的公共代码
        public const uint ME_DATA_MDLWRITE = (ME_DATA_ERR | 0xA00); // 模块写入错误
        public const uint ME_DATA_MDLWRITE_ADDR = (ME_DATA_MDLWRITE | ME_DATA_ADDR); // 地址写入错误
        public const uint ME_DATA_MDLWRITE_NOTREGIST = (ME_DATA_MDLWRITE | 0x1); // 未注册错误
        public const uint ME_DATA_MDLWRITE_DATASIZE = (ME_DATA_MDLWRITE | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_DATA_MDLWRITE_DATATYPE = (ME_DATA_MDLWRITE | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_DATA_MDLWRITE_WRITEONLY = (ME_DATA_MDLWRITE | ME_DATA_WRITEONLY); // 只写错误
        public const uint ME_DATA_MDLWRITE_READONLY = (ME_DATA_MDLWRITE | ME_DATA_READONLY); // 只读错误
        public const uint ME_DATA_MDLWRITE_WRITE = (ME_DATA_MDLWRITE | ME_DATA_WRITEERR); // 写入错误

        // 重线程写入相关的公共代码
        public const uint ME_DATA_RETHREADWRITE = (ME_DATA_ERR | 0xB00); // 重线程写入错误
        public const uint ME_DATA_RETHREADWRITE_NODATA = (ME_DATA_RETHREADWRITE | ME_DATA_NODATA); // 无数据错误

        // TLFGROUP相关的公共代码
        public const uint ME_DATA_TLFGROUP = (ME_DATA_ERR | 0x1000); // TLFGROUP错误
        public const uint ME_DATA_TLFGROUP_ADDR = (ME_DATA_TLFGROUP | ME_DATA_ADDR); // 地址错误
        public const uint ME_DATA_TLFGROUP_EXIST = (ME_DATA_TLFGROUP | 0x91); // 存在错误
        public const uint ME_DATA_TLFGROUP_NONEXIST = (ME_DATA_TLFGROUP | 0x92); // 不存在错误
        public const uint ME_DATA_TLFGROUP_OVER = (ME_DATA_TLFGROUP | 0x93); // 超出错误
        public const uint ME_DATA_TLFGROUP_NONFORMAT = (ME_DATA_TLFGROUP | 0x94); // 非格式错误
        public const uint ME_DATA_TLFGROUP_UNMACH = (ME_DATA_TLFGROUP | 0x96); // 不匹配错误
        public const uint ME_DATA_TLFGROUP_OUTOFSPEC = (ME_DATA_TLFGROUP | 0x97); // 超出规格错误

        // TLFTOOL相关的公共代码
        public const uint ME_DATA_TLFTOOL = (ME_DATA_ERR | 0x1100); // TLFTOOL错误
        public const uint ME_DATA_TLFTOOL_ADDR = (ME_DATA_TLFTOOL | ME_DATA_ADDR); // 地址错误
        public const uint ME_DATA_TLFTOOL_EXIST = (ME_DATA_TLFTOOL | 0x91); // 存在错误
        public const uint ME_DATA_TLFTOOL_NONEXIST = (ME_DATA_TLFTOOL | 0x92); // 不存在错误
        public const uint ME_DATA_TLFTOOL_OVER = (ME_DATA_TLFTOOL | 0x93); // 超出错误
        public const uint ME_DATA_TLFTOOL_PARAMERR = (ME_DATA_TLFTOOL | 0x94); // 参数错误
        public const uint ME_DATA_TLFTOOL_MAXMINERR = (ME_DATA_TLFTOOL | 0x95); // 最大最小错误
        public const uint ME_DATA_TLFTOOL_UNMACH = (ME_DATA_TLFTOOL | 0x96); // 不匹配错误
        public const uint ME_DATA_TLFTOOL_OUTOFSPEC = (ME_DATA_TLFTOOL | 0x97); // 超出规格错误




        // 操作相关的公共代码
        public const uint ME_OPE = 0x50000; // 操作
        public const uint ME_OPE_ERR = (ME_ERR_FLG | ME_OPE); // 操作错误

        // 重置相关的操作代码
        public const uint ME_OPE_RESET = (ME_OPE_ERR | 0x100); // 重置错误
        public const uint ME_OPE_RESET_EXEC = (ME_OPE_RESET | 0x1); // 执行重置错误
        public const uint ME_OPE_RESET_ADDR = (ME_OPE_RESET | ME_OPE_ADDR); // 地址重置错误
        public const uint ME_OPE_RESET_MODE = (ME_OPE_RESET | ME_OPE_MODE); // 模式重置错误
        public const uint ME_OPE_RESET_LOCALMODE = (ME_OPE_RESET | ME_OPE_LOCALMODE); // 本地模式重置错误
        public const uint ME_OPE_RESET_EXTERNAL = (ME_OPE_RESET | ME_OPE_EXTERNAL); // 外部重置错误

        // 块停止相关的操作代码
        public const uint ME_OPE_BLKSTOP = (ME_OPE_ERR | 0x200); // 块停止错误
        public const uint ME_OPE_BLKSTOP_ADDR = (ME_OPE_BLKSTOP | ME_OPE_ADDR); // 地址停止错误
        public const uint ME_OPE_BLKSTOP_MODE = (ME_OPE_BLKSTOP | ME_OPE_MODE); // 模式停止错误
        public const uint ME_OPE_BLKSTOP_LOCALMODE = (ME_OPE_BLKSTOP | ME_OPE_LOCALMODE); // 本地模式停止错误
        public const uint ME_OPE_BLKSTOP_EXTERNAL = (ME_OPE_BLKSTOP | ME_OPE_EXTERNAL); // 外部停止错误

        // 操作模式相关的操作代码
        public const uint ME_OPE_OPEMODE = (ME_OPE_ERR | 0x300); // 操作模式错误
        public const uint ME_OPE_OPEMODE_ADDR = (ME_OPE_OPEMODE | ME_OPE_ADDR); // 地址操作模式错误
        public const uint ME_OPE_OPEMODE_MODE = (ME_OPE_OPEMODE | ME_OPE_MODE); // 模式操作错误
        public const uint ME_OPE_OPEMODE_LOCALMODE = (ME_OPE_OPEMODE | ME_OPE_LOCALMODE); // 本地模式操作错误
        public const uint ME_OPE_OPEMODE_EXTERNAL = (ME_OPE_OPEMODE | ME_OPE_EXTERNAL); // 外部操作模式错误
        public const uint ME_OPE_OPEMODE_OPERATINGAXIS = (ME_OPE_OPEMODE | ME_OPE_OPERATINGAXIS); // 操作轴错误

        // 操作覆盖相关的操作代码
        public const uint ME_OPE_OVERRIDE = (ME_OPE_ERR | 0x400); // 操作覆盖错误
        public const uint ME_OPE_OVERRIDE_ADDR = (ME_OPE_OVERRIDE | ME_OPE_ADDR); // 地址覆盖错误
        public const uint ME_OPE_OVERRIDE_VALUE = (ME_OPE_OVERRIDE | ME_OPE_VALUE); // 值覆盖错误
        public const uint ME_OPE_OVERRIDE_LOCALMODE = (ME_OPE_OVERRIDE | ME_OPE_LOCALMODE); // 本地模式覆盖错误
        public const uint ME_OPE_OVERRIDE_EXTERNAL = (ME_OPE_OVERRIDE | ME_OPE_EXTERNAL); // 外部覆盖错误

        // 操作速度相关的操作代码
        public const uint ME_OPE_MSPEED = (ME_OPE_ERR | 0x500); // 操作速度错误
        public const uint ME_OPE_MSPEED_ADDR = (ME_OPE_MSPEED | ME_OPE_ADDR); // 地址速度错误
        public const uint ME_OPE_MSPEED_VALUE = (ME_OPE_MSPEED | ME_OPE_VALUE); // 值速度错误
        public const uint ME_OPE_MSPEED_LOCALMODE = (ME_OPE_MSPEED | ME_OPE_LOCALMODE); // 本地模式速度错误
        public const uint ME_OPE_MSPEED_EXTERNAL = (ME_OPE_MSPEED | ME_OPE_EXTERNAL); // 外部速度错误

        // 操作处理相关的操作代码
        public const uint ME_OPE_HNDLMAG = (ME_OPE_ERR | 0x600); // 操作处理错误
        public const uint ME_OPE_HNDLMAG_ADDR = (ME_OPE_HNDLMAG | ME_OPE_ADDR); // 地址处理错误
        public const uint ME_OPE_HNDLMAG_VALUE = (ME_OPE_HNDLMAG | ME_OPE_VALUE); // 值处理错误
        public const uint ME_OPE_HNDLMAG_LOCALMODE = (ME_OPE_HNDLMAG | ME_OPE_LOCALMODE); // 本地模式处理错误
        public const uint ME_OPE_HNDLMAG_EXTERNAL = (ME_OPE_HNDLMAG | ME_OPE_EXTERNAL); // 外部处理错误

        // 启动轴相关的操作代码
        public const uint ME_OPE_STARTAXOPE = (ME_OPE_ERR | 0x700); // 启动轴操作错误
        public const uint ME_OPE_STARTAXOPE_OPEMODE = (ME_OPE_STARTAXOPE | 0x1); // 启动轴操作模式错误
        public const uint ME_OPE_STARTAXOPE_ADDR = (ME_OPE_STARTAXOPE | ME_OPE_ADDR); // 地址启动轴错误
        public const uint ME_OPE_STARTAXOPE_MODE = (ME_OPE_STARTAXOPE | ME_OPE_MODE); // 模式启动轴错误
        public const uint ME_OPE_STARTAXOPE_LOCALMODE = (ME_OPE_STARTAXOPE | ME_OPE_LOCALMODE); // 本地模式启动轴错误
        public const uint ME_OPE_STARTAXOPE_EXTERNAL = (ME_OPE_STARTAXOPE | ME_OPE_EXTERNAL); // 外部启动轴错误
        public const uint ME_OPE_STARTAXOPE_OPERATINGAXIS = (ME_OPE_STARTAXOPE | ME_OPE_OPERATINGAXIS); // 操作轴启动错误

        // 运动控制PLC相关的操作代码
        public const uint ME_OPE_TRANSPLC = (ME_OPE_ERR | 0x800); // PLC传输错误
        public const uint ME_OPE_TRANSPLC_READ = (ME_OPE_TRANSPLC | 0x1); // 读取PLC错误
        public const uint ME_OPE_TRANSPLC_WRITE = (ME_OPE_TRANSPLC | 0x2); // 写入PLC错误
        public const uint ME_OPE_TRANSPLC_ADDR = (ME_OPE_TRANSPLC | ME_OPE_ADDR); // 地址PLC错误
        public const uint ME_OPE_TRANSPLC_MODE = (ME_OPE_TRANSPLC | ME_OPE_MODE); // 模式PLC错误
        public const uint ME_OPE_TRANSPLC_PLCRUN = (ME_OPE_TRANSPLC | ME_COMMON_PLCRUN); // PLC运行错误

        // 激活PLC相关的操作代码
        public const uint ME_OPE_ACTPLC = (ME_OPE_ERR | 0x900); // 激活PLC错误
        public const uint ME_OPE_ACTPLC_ADDR = (ME_OPE_ACTPLC | ME_OPE_ADDR); // 地址激活PLC错误
        public const uint ME_OPE_ACTPLC_MODE = (ME_OPE_ACTPLC | ME_OPE_MODE); // 模式激活PLC错误

        // 采样相关的操作代码
        public const uint ME_OPE_SAMPLING = (ME_OPE_ERR | 0xA00); // 采样错误
        public const uint ME_OPE_SAMPLING_ALREADY = (ME_OPE_SAMPLING | ME_COMMON_SAMPLINGACTIVE); // 已经采样错误
        public const uint ME_OPE_SAMPLING_NOT = (ME_OPE_SAMPLING | ME_OPE_NOTSAMPLING); // 未采样错误
        public const uint ME_OPE_SAMPLING_ADDR = (ME_OPE_SAMPLING | ME_OPE_ADDR); // 地址采样错误
        public const uint ME_OPE_SAMPLING_MODE = (ME_OPE_SAMPLING | ME_OPE_MODE); // 模式采样错误
        public const uint ME_OPE_SAMPLING_PARAM = (ME_OPE_SAMPLING | ME_OPE_PARAM); // 参数采样错误

        // 获取程序点相关的操作代码
        public const uint ME_OPE_GETPRGPOINT = (ME_OPE_ERR | 0xB00); // 获取程序点错误
        public const uint ME_OPE_GETPRGPOINT_ADDR = (ME_OPE_GETPRGPOINT | ME_OPE_ADDR); // 地址获取程序点错误
        public const uint ME_OPE_GETPRGPOINT_DATASIZE = (ME_OPE_GETPRGPOINT | ME_OPE_DATASIZE); // 数据大小获取程序点错误
        public const uint ME_OPE_GETPRGPOINT_DATATYPE = (ME_OPE_GETPRGPOINT | ME_OPE_DATATYPE); // 数据类型获取程序点错误

        // 获取程序块相关的操作代码
        public const uint ME_OPE_GETPRGBLK = (ME_OPE_ERR | 0xC00); // 获取程序块错误
        public const uint ME_OPE_GETPRGBLK_NOS = (ME_OPE_GETPRGBLK | 0x1); // 无程序块错误
        public const uint ME_OPE_GETPRGBLK_NOSEARCH = (ME_OPE_GETPRGBLK | 0x2); // 不搜索错误
        public const uint ME_OPE_GETPRGBLK_DATAERR = (ME_OPE_GETPRGBLK | 0x3); // 数据错误
        public const uint ME_OPE_GETPRGBLK_ADDR = (ME_OPE_GETPRGBLK | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_GETPRGBLK_DATASIZE = (ME_OPE_GETPRGBLK | ME_OPE_DATASIZE); // 数据大小错误
        public const uint ME_OPE_GETPRGBLK_DATATYPE = (ME_OPE_GETPRGBLK | ME_OPE_DATATYPE); // 数据类型错误

        // 当前报警相关的操作代码
        public const uint ME_OPE_CURRALM = (ME_OPE_ERR | 0xD00); // 当前报警错误
        public const uint ME_OPE_CURRALM_NOS = (ME_OPE_CURRALM | 0x1); // 无当前报警错误
        public const uint ME_OPE_CURRALM_ALMTYPE = (ME_OPE_CURRALM | 0x2); // 报警类型错误
        public const uint ME_OPE_CURRALM_DATAERR = (ME_OPE_CURRALM | 0x3); // 数据错误
        public const uint ME_OPE_CURRALM_NOALM = (ME_OPE_CURRALM | 0x4); // 无报警错误
        public const uint ME_OPE_CURRALM_ADDR = (ME_OPE_CURRALM | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_CURRALM_DATASIZE = (ME_OPE_CURRALM | ME_OPE_DATASIZE); // 数据大小错误
        public const uint ME_OPE_CURRALM_DATATYPE = (ME_OPE_CURRALM | ME_OPE_DATATYPE); // 数据类型错误

        // 报警消息相关的操作代码
        public const uint ME_OPE_ALMMSG = (ME_OPE_ERR | 0xE00); // 报警消息错误
        public const uint ME_OPE_ALMMSG_ALMNUM = (ME_OPE_ALMMSG | 0x1); // 报警编号错误
        public const uint ME_OPE_ALMMSG_ALMTYPE = (ME_OPE_ALMMSG | 0x2); // 报警类型错误
        public const uint ME_OPE_ALMMSG_ADDR = (ME_OPE_ALMMSG | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_ALMMSG_DATASIZE = (ME_OPE_ALMMSG | ME_OPE_DATASIZE); // 数据大小错误
        public const uint ME_OPE_ALMMSG_DATATYPE = (ME_OPE_ALMMSG | ME_OPE_DATATYPE); // 数据类型错误

        // 启动操作相关的操作代码
        public const uint ME_OPE_STARTOPE = (ME_OPE_ERR | 0xF00); // 启动操作错误
        public const uint ME_OPE_STARTOPE_PRGFORMAT = (ME_OPE_STARTOPE | 0x1); // 程序格式错误
        public const uint ME_OPE_STARTOPE_FILESYSTEM = (ME_OPE_STARTOPE | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_STARTOPE_ADDR = (ME_OPE_STARTOPE | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_STARTOPE_NOPRG = (ME_OPE_STARTOPE | ME_COMMON_NOFILE); // 无程序错误
        public const uint ME_OPE_STARTOPE_RESET = (ME_OPE_STARTOPE | ME_OPE_RESETACTIVE); // 重置错误
        public const uint ME_OPE_STARTOPE_EMG = (ME_OPE_STARTOPE | ME_OPE_EMG); // 紧急错误
        public const uint ME_OPE_STARTOPE_RESETWAIT = (ME_OPE_STARTOPE | ME_OPE_RESETWAIT); // 重置等待错误
        public const uint ME_OPE_STARTOPE_LOCALMODE = (ME_OPE_STARTOPE | ME_OPE_LOCALMODE); // 本地模式错误
        public const uint ME_OPE_STARTOPE_EXTERNAL = (ME_OPE_STARTOPE | ME_OPE_EXTERNAL); // 外部错误
        public const uint ME_OPE_STARTOPE_DATATYPE = (ME_OPE_STARTOPE | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_STARTOPE_DIRECTMODE = (ME_OPE_STARTOPE | ME_OPE_DIRECTMODE); // 直接模式错误

        // 选择程序相关的操作代码
        public const uint ME_OPE_SELECTPRG = (ME_OPE_ERR | 0x1000); // 选择程序错误
        public const uint ME_OPE_SELECTPRG_PRGFORMAT = (ME_OPE_SELECTPRG | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_SELECTPRG_NOTPRG = (ME_OPE_SELECTPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_SELECTPRG_NOPRG = (ME_OPE_SELECTPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_SELECTPRG_RUNNING = (ME_OPE_SELECTPRG | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_SELECTPRG_RESET = (ME_OPE_SELECTPRG | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_SELECTPRG_LONGPATH = (ME_OPE_SELECTPRG | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_SELECTPRG_NCPCCOM = (ME_OPE_SELECTPRG | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_SELECTPRG_TIMEOUT = (ME_OPE_SELECTPRG | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_SELECTPRG_SEARCHING = (ME_OPE_SELECTPRG | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_SELECTPRG_CHECKING = (ME_OPE_SELECTPRG | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_SELECTPRG_FILEREAD = (ME_OPE_SELECTPRG | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_SELECTPRG_FILEWRITE = (ME_OPE_SELECTPRG | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_SELECTPRG_FILESYSTEM = (ME_OPE_SELECTPRG | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_SELECTPRG_DATATYPE = (ME_OPE_SELECTPRG | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_SELECTPRG_ADDR = (ME_OPE_SELECTPRG | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SELECTPRG_MODE = (ME_OPE_SELECTPRG | ME_OPE_MODE); // 模式错误
        public const uint ME_OPE_SELECTPRG_NOTSUPPORTED = (ME_OPE_SELECTPRG | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_SELECTPRG_PGLKC = (ME_OPE_SELECTPRG | ME_COMMON_PGLKC); // 保护错误
        public const uint ME_OPE_SELECTPRG_ILLEGALPOS = (ME_OPE_SELECTPRG | ME_OPESRC_ILLEGALPOS); // 非法位置错误
        public const uint ME_OPE_SELECTPRG_REVERSE = (ME_OPE_SELECTPRG | ME_OPESRC_REVERSE); // 反向错误
        public const uint ME_OPE_SELECTPRG_PROTECT = (ME_OPE_SELECTPRG | ME_COMMON_PROTECT); // 保护错误

        // 远程/本地模式相关的操作代码
        public const uint ME_OPE_RMTLCLMODE = (ME_OPE_ERR | 0x1100); // 远程/本地模式错误
        public const uint ME_OPE_RMTLCLMODE_LOCALMODE = (ME_OPE_RMTLCLMODE | ME_OPE_LOCALMODE); // 本地模式错误
        public const uint ME_OPE_RMTLCLMODE_ADDR = (ME_OPE_RMTLCLMODE | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_RMTLCLMODE_MODE = (ME_OPE_RMTLCLMODE | ME_OPE_MODE); // 模式错误

        // 发送命令相关的操作代码
        public const uint ME_OPE_SENDCMD = (ME_OPE_ERR | 0x1200); // 发送命令错误
        public const uint ME_OPE_SENDCMD_ADDR = (ME_OPE_SENDCMD | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SENDCMD_DATATYPE = (ME_OPE_SENDCMD | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_SENDCMD_DATASIZE = (ME_OPE_SENDCMD | ME_OPE_DATASIZE); // 数据大小错误

        // 模拟设置模式相关的操作代码
        public const uint ME_OPE_SIMUSETMODE = (ME_OPE_ERR | 0x1300); // 模拟设置模式错误
        public const uint ME_OPE_SIMUSETMODE_ADDR = (ME_OPE_SIMUSETMODE | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SIMUSETMODE_PARAM = (ME_OPE_SIMUSETMODE | ME_OPE_PARAM); // 参数错误
        public const uint ME_OPE_SIMUSETMODE_START = (ME_OPE_SIMUSETMODE | 0x1); // 启动模拟设置错误
        public const uint ME_OPE_SIMUSETMODE_THRDCHKSTART = (ME_OPE_SIMUSETMODE | 0x2); // 启动线程检查错误

        // 模拟重置模式相关的操作代码
        public const uint ME_OPE_SIMURESETMODE = (ME_OPE_ERR | 0x1400); // 模拟重置模式错误
        public const uint ME_OPE_SIMURESETMODE_NOTREGIST = (ME_OPE_SIMURESETMODE | 0x1); // ID未注册错误
        public const uint ME_OPE_SIMURESETMODE_END = (ME_OPE_SIMURESETMODE | 0x2); // 模拟重置模式结束错误

        // 模拟读取相关的操作代码
        public const uint ME_OPE_SIMUREAD = (ME_OPE_ERR | 0x1500); // 模拟读取错误
        public const uint ME_OPE_SIMUREAD_NOTREGIST = (ME_OPE_SIMUREAD | 0x1); // ID未注册错误
        public const uint ME_OPE_SIMUREAD_READ = (ME_OPE_SIMUREAD | 0x2); // 读取错误
        public const uint ME_OPE_SIMUREAD_THRDCHKNOMODE = (ME_OPE_SIMUREAD | 0x3); // 3D检查模式无效错误

        // 重新启动程序相关的操作代码
        public const uint ME_OPE_RSTARTPRG = (ME_OPE_ERR | 0x1600); // 重新启动程序错误
        public const uint ME_OPE_RSTARTPRG_PRGFORMAT = (ME_OPE_RSTARTPRG | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_RSTARTPRG_NOPRG = (ME_OPE_RSTARTPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_RSTARTPRG_RUNNING = (ME_OPE_RSTARTPRG | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_RSTARTPRG_RESET = (ME_OPE_RSTARTPRG | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_RSTARTPRG_LONGPATH = (ME_OPE_RSTARTPRG | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_RSTARTPRG_NCPCCOM = (ME_OPE_RSTARTPRG | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_RSTARTPRG_TIMEOUT = (ME_OPE_RSTARTPRG | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_RSTARTPRG_NBNOTFOUND = (ME_OPE_RSTARTPRG | ME_OPESRC_NBNOTFOUND); // 未找到错误
        public const uint ME_OPE_RSTARTPRG_TOPSEARCH = (ME_OPE_RSTARTPRG | ME_OPESRC_TOPSEARCH); // 顶部搜索错误
        public const uint ME_OPE_RSTARTPRG_SEARCHING = (ME_OPE_RSTARTPRG | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_RSTARTPRG_CHECKING = (ME_OPE_RSTARTPRG | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_RSTARTPRG_ALREADYSEARCHED = (ME_OPE_RSTARTPRG | ME_OPESRC_ALREADYSEARCHED); // 已搜索错误
        public const uint ME_OPE_RSTARTPRG_OTHERSEARCHING = (ME_OPE_RSTARTPRG | ME_OPESRC_OTHERSEARCHING); // 其他正在搜索错误
        public const uint ME_OPE_RSTARTPRG_FILEREAD = (ME_OPE_RSTARTPRG | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_RSTARTPRG_FILEWRITE = (ME_OPE_RSTARTPRG | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_RSTARTPRG_FILESYSTEM = (ME_OPE_RSTARTPRG | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_RSTARTPRG_DATATYPE = (ME_OPE_RSTARTPRG | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_RSTARTPRG_ADDR = (ME_OPE_RSTARTPRG | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_RSTARTPRG_EMG = (ME_OPE_RSTARTPRG | ME_OPE_EMG); // 紧急错误
        public const uint ME_OPE_RSTARTPRG_NOTSUPPORTED = (ME_OPE_RSTARTPRG | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_RSTARTPRG_PGLKC = (ME_OPE_RSTARTPRG | ME_COMMON_PGLKC); // 保护错误
        public const uint ME_OPE_RSTARTPRG_SORT = (ME_OPE_RSTARTPRG | ME_OPESRC_SORT); // 排序错误

        // 重新启动程序相关的操作代码
        public const uint ME_OPE_RSTRPRG = (ME_OPE_ERR | 0x1600); // 重新启动程序错误
        public const uint ME_OPE_RSTRPRG_PRGFORMAT = (ME_OPE_RSTRPRG | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_RSTRPRG_NOPRG = (ME_OPE_RSTRPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_RSTRPRG_RUNNING = (ME_OPE_RSTRPRG | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_RSTRPRG_RESET = (ME_OPE_RSTRPRG | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_RSTRPRG_LONGPATH = (ME_OPE_RSTRPRG | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_RSTRPRG_NCPCCOM = (ME_OPE_RSTRPRG | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_RSTRPRG_TIMEOUT = (ME_OPE_RSTRPRG | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_RSTRPRG_NBNOTFOUND = (ME_OPE_RSTRPRG | ME_OPESRC_NBNOTFOUND); // 未找到错误
        public const uint ME_OPE_RSTRPRG_TOPSEARCH = (ME_OPE_RSTRPRG | ME_OPESRC_TOPSEARCH); // 顶部搜索错误
        public const uint ME_OPE_RSTRPRG_SEARCHING = (ME_OPE_RSTRPRG | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_RSTRPRG_CHECKING = (ME_OPE_RSTRPRG | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_RSTRPRG_ALREADYSEARCHED = (ME_OPE_RSTRPRG | ME_OPESRC_ALREADYSEARCHED); // 已搜索错误
        public const uint ME_OPE_RSTRPRG_OTHERSEARCHING = (ME_OPE_RSTRPRG | ME_OPESRC_ALREADYSEARCHED); // 其他正在搜索错误
        public const uint ME_OPE_RSTRPRG_FILEREAD = (ME_OPE_RSTRPRG | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_RSTRPRG_FILEWRITE = (ME_OPE_RSTRPRG | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_RSTRPRG_FILESYSTEM = (ME_OPE_RSTRPRG | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_RSTRPRG_DATATYPE = (ME_OPE_RSTRPRG | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_RSTRPRG_ADDR = (ME_OPE_RSTRPRG | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_RSTRPRG_EMG = (ME_OPE_RSTRPRG | ME_OPE_EMG); // 紧急错误
        public const uint ME_OPE_RSTRPRG_NOTSUPPORTED = (ME_OPE_RSTRPRG | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_RSTRPRG_PGLKC = (ME_OPE_RSTRPRG | ME_COMMON_PGLKC); // 保护错误

        // 缓冲区编辑相关的操作代码
        public const uint ME_OPE_BUFFEDIT = (ME_OPE_ERR | 0x1700); // 缓冲区编辑错误
        public const uint ME_OPE_BUFFEDIT_OPTION = (ME_OPE_BUFFEDIT | 0x1); // 选项错误
        public const uint ME_OPE_BUFFEDIT_ALREADYOPEN = (ME_OPE_BUFFEDIT | 0x2); // 已打开错误
        public const uint ME_OPE_BUFFEDIT_RUNNING = (ME_OPE_BUFFEDIT | 0x3); // 运行中错误
        public const uint ME_OPE_BUFFEDIT_DEVICE = (ME_OPE_BUFFEDIT | 0x4); // 设备错误
        public const uint ME_OPE_BUFFEDIT_NCPCCOM = (ME_OPE_BUFFEDIT | 0x5); // PC通信错误
        public const uint ME_OPE_BUFFEDIT_TIMEOUT = (ME_OPE_BUFFEDIT | 0x6); // 超时错误
        public const uint ME_OPE_BUFFEDIT_PRGSTOP = (ME_OPE_BUFFEDIT | 0x7); // 程序停止错误
        public const uint ME_OPE_BUFFEDIT_NOBLOCK = (ME_OPE_BUFFEDIT | 0x8); // 无阻塞错误
        public const uint ME_OPE_BUFFEDIT_RUNNING2 = (ME_OPE_BUFFEDIT | 0x9); // 运行中错误（第二个）
        public const uint ME_OPE_BUFFEDIT_NESTING = (ME_OPE_BUFFEDIT | 0xA); // 嵌套错误
        public const uint ME_OPE_BUFFEDIT_NSEARCH = (ME_OPE_BUFFEDIT | 0xB); // 搜索错误
        public const uint ME_OPE_BUFFEDIT_WRITING = (ME_OPE_BUFFEDIT | 0xC); // 正在写入错误
        public const uint ME_OPE_BUFFEDIT_NOS = (ME_OPE_BUFFEDIT | 0xD); // 无效操作错误
        public const uint ME_OPE_BUFFEDIT_DATAERR = (ME_OPE_BUFFEDIT | 0xE); // 数据错误
        public const uint ME_OPE_BUFFEDIT_SIZEOVER = (ME_OPE_BUFFEDIT | 0xF); // 大小超出错误
        public const uint ME_OPE_BUFFEDIT_NOTGETTING = (ME_OPE_BUFFEDIT | 0x10); // 未获取错误
        public const uint ME_OPE_BUFFEDIT_NOTSETTING = (ME_OPE_BUFFEDIT | 0x11); // 未设置错误
        public const uint ME_OPE_BUFFEDIT_DATAFORMAT = (ME_OPE_BUFFEDIT | 0x12); // 数据格式错误
        public const uint ME_OPE_BUFFEDIT_FILESYSTEM = (ME_OPE_BUFFEDIT | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_BUFFEDIT_PROTECT = (ME_OPE_BUFFEDIT | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_OPE_BUFFEDIT_ADDR = (ME_OPE_BUFFEDIT | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_BUFFEDIT_MODE = (ME_OPE_BUFFEDIT | ME_OPE_MODE); // 模式错误
        public const uint ME_OPE_BUFFEDIT_DATASIZE = (ME_OPE_BUFFEDIT | ME_OPE_DATASIZE); // 数据大小错误
        public const uint ME_OPE_BUFFEDIT_DATATYPE = (ME_OPE_BUFFEDIT | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_BUFFEDIT_PGLKC = (ME_OPE_BUFFEDIT | ME_COMMON_PGLKC); // 保护错误
        public const uint ME_OPE_BUFFEDIT_WRITE_PROTECT = (ME_OPE_BUFFEDIT | ME_FILE_WRITE_PROTECT); // 写保护错误


        // 选择程序扩展相关的操作代码
        public const uint ME_OPE_SELECTPRGEX = (ME_OPE_ERR | 0x1800); // 选择程序扩展错误
        public const uint ME_OPE_SELECTPRGEX_PRGFORMAT = (ME_OPE_SELECTPRGEX | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_SELECTPRGEX_NOPRG = (ME_OPE_SELECTPRGEX | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_SELECTPRGEX_RUNNING = (ME_OPE_SELECTPRGEX | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_SELECTPRGEX_LONGPATH = (ME_OPE_SELECTPRGEX | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_SELECTPRGEX_NCPCCOM = (ME_OPE_SELECTPRGEX | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_SELECTPRGEX_TIMEOUT = (ME_OPE_SELECTPRGEX | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_SELECTPRGEX_SEARCHING = (ME_OPE_SELECTPRGEX | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_SELECTPRGEX_CHECKING = (ME_OPE_SELECTPRGEX | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_SELECTPRGEX_FILEREAD = (ME_OPE_SELECTPRGEX | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_SELECTPRGEX_FILEWRITE = (ME_OPE_SELECTPRGEX | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_SELECTPRGEX_FILESYSTEM = (ME_OPE_SELECTPRGEX | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_SELECTPRGEX_DATATYPE = (ME_OPE_SELECTPRGEX | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_SELECTPRGEX_ADDR = (ME_OPE_SELECTPRGEX | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SELECTPRGEX_NOTSUPPORTED = (ME_OPE_SELECTPRGEX | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_SELECTPRGEX_PGLKC = (ME_OPE_SELECTPRGEX | ME_COMMON_PGLKC); // 保护错误
        public const uint ME_OPE_SELECTPRGEX_ILLEGALPOS = (ME_OPE_SELECTPRGEX | ME_OPESRC_ILLEGALPOS); // 非法位置错误
        public const uint ME_OPE_SELECTPRGEX_REVERSE = (ME_OPE_SELECTPRGEX | ME_OPESRC_REVERSE); // 反向错误
        public const uint ME_OPE_SELECTPRGEX_PROTECT = (ME_OPE_SELECTPRGEX | ME_COMMON_PROTECT); // 保护错误

        // 获取程序栈相关的操作代码
        public const uint ME_OPE_GETPRGSTAK = (ME_OPE_ERR | 0x1900); // 获取程序栈错误
        public const uint ME_OPE_GETPRGSTAK_NOTSUPPORTED = (ME_OPE_GETPRGSTAK | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_GETPRGSTAK_ADDR = (ME_OPE_GETPRGSTAK | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_GETPRGSTAK_DATASIZE = (ME_OPE_GETPRGSTAK | ME_OPE_DATASIZE); // 数据大小错误

        // 排序相关的操作代码
        public const uint ME_OPE_COLLATION = (ME_OPE_ERR | 0x1A00); // 排序错误
        public const uint ME_OPE_COLLATION_PRGFORMAT = (ME_OPE_COLLATION | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_COLLATION_NOPRG = (ME_OPE_COLLATION | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_COLLATION_RUNNING = (ME_OPE_COLLATION | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_COLLATION_RESET = (ME_OPE_COLLATION | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_COLLATION_FILESYSTEM = (ME_OPE_COLLATION | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_COLLATION_SEARCHING = (ME_OPE_COLLATION | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_COLLATION_DATATYPE = (ME_OPE_COLLATION | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_COLLATION_ADDR = (ME_OPE_COLLATION | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_COLLATION_NOTSUPPORTED = (ME_OPE_COLLATION | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_COLLATION_CHECKING = (ME_OPE_COLLATION | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_COLLATION_LONGPATH = (ME_OPE_COLLATION | ME_OPESRC_LONGPATH); // 长路径错误

        // 检查程序相关的操作代码
        public const uint ME_OPE_CHECKPRG = (ME_OPE_ERR | 0x1B00); // 检查程序错误
        public const uint ME_OPE_CHECKPRG_PRGFORMAT = (ME_OPE_CHECKPRG | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_CHECKPRG_NOPRG = (ME_OPE_CHECKPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_CHECKPRG_NOTPRG = (ME_OPE_CHECKPRG | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_CHECKPRG_RUNNING = (ME_OPE_CHECKPRG | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_CHECKPRG_RESET = (ME_OPE_CHECKPRG | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_CHECKPRG_LONGPATH = (ME_OPE_CHECKPRG | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_CHECKPRG_NCPCCOM = (ME_OPE_CHECKPRG | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_CHECKPRG_TIMEOUT = (ME_OPE_CHECKPRG | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_CHECKPRG_SEARCHING = (ME_OPE_CHECKPRG | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_CHECKPRG_CHECKING = (ME_OPE_CHECKPRG | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_CHECKPRG_FILEREAD = (ME_OPE_CHECKPRG | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_CHECKPRG_FILEWRITE = (ME_OPE_CHECKPRG | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_CHECKPRG_FILESYSTEM = (ME_OPE_CHECKPRG | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_CHECKPRG_DATATYPE = (ME_OPE_CHECKPRG | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_CHECKPRG_ADDR = (ME_OPE_CHECKPRG | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_CHECKPRG_NOTSUPPORTED = (ME_OPE_CHECKPRG | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_OPE_CHECKPRG_PGLKC = (ME_OPE_CHECKPRG | ME_COMMON_PGLKC); // 保护错误


        // 底部检查相关操作代码
        public const uint ME_OPE_BOTTOMCHK = (ME_OPE_ERR | 0x1C00); // 底部检查错误
        public const uint ME_OPE_BOTTOMCHK_PRGFORMAT = (ME_OPE_BOTTOMCHK | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_BOTTOMCHK_NOPRG = (ME_OPE_BOTTOMCHK | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_BOTTOMCHK_RUNNING = (ME_OPE_BOTTOMCHK | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_BOTTOMCHK_RESET = (ME_OPE_BOTTOMCHK | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_BOTTOMCHK_LONGPATH = (ME_OPE_BOTTOMCHK | ME_OPESRC_LONGPATH); // 长路径错误
        public const uint ME_OPE_BOTTOMCHK_NCPCCOM = (ME_OPE_BOTTOMCHK | ME_OPESRC_NCPCCOM); // ncpccom.exe错误
        public const uint ME_OPE_BOTTOMCHK_TIMEOUT = (ME_OPE_BOTTOMCHK | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_BOTTOMCHK_SEARCHING = (ME_OPE_BOTTOMCHK | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_BOTTOMCHK_CHECKING = (ME_OPE_BOTTOMCHK | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_BOTTOMCHK_TOPSEARCH = (ME_OPE_BOTTOMCHK | ME_OPESRC_TOPSEARCH); // 正在顶部搜索错误
        public const uint ME_OPE_BOTTOMCHK_FILEREAD = (ME_OPE_BOTTOMCHK | ME_FILE_READERR); // 文件读取错误
        public const uint ME_OPE_BOTTOMCHK_FILEWRITE = (ME_OPE_BOTTOMCHK | ME_FILE_WRITEERR); // 文件写入错误
        public const uint ME_OPE_BOTTOMCHK_FILESYSTEM = (ME_OPE_BOTTOMCHK | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_BOTTOMCHK_DATATYPE = (ME_OPE_BOTTOMCHK | ME_OPE_DATATYPE); // 数据类型错误
        public const uint ME_OPE_BOTTOMCHK_ADDR = (ME_OPE_BOTTOMCHK | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_BOTTOMCHK_NOTSUPPORTED = (ME_OPE_BOTTOMCHK | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误

        // 重置检查相关操作代码
        public const uint ME_OPE_RESETCHECK = (ME_OPE_ERR | 0x1D00); // 重置检查错误
        public const uint ME_OPE_RESETCHECK_SEARCHING = (ME_OPE_RESETCHECK | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_RESETCHECK_RUNNING = (ME_OPE_RESETCHECK | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_RESETCHECK_ADDR = (ME_OPE_RESETCHECK | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_RESETCHECK_CHECKING = (ME_OPE_RESETCHECK | ME_OPESRC_CHECKING); // 正在检查错误

        // 执行检查相关操作代码
        public const uint ME_OPE_EXECCHECK = (ME_OPE_ERR | 0x1E00); // 执行检查错误
        public const uint ME_OPE_EXECCHECK_RESET = (ME_OPE_EXECCHECK | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_EXECCHECK_TIMEOUT = (ME_OPE_EXECCHECK | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_EXECCHECK_TOPSEARCH = (ME_OPE_EXECCHECK | ME_OPESRC_TOPSEARCH); // 正在顶部搜索错误
        public const uint ME_OPE_EXECCHECK_SEARCHING = (ME_OPE_EXECCHECK | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_EXECCHECK_RUNNING = (ME_OPE_EXECCHECK | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_EXECCHECK_ADDR = (ME_OPE_EXECCHECK | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_EXECCHECK_PRGERR = (ME_OPE_EXECCHECK | 0x20); // 程序错误
        public const uint ME_OPE_EXECCHECK_ERRREQUEST = (ME_OPE_EXECCHECK | 0x21); // 请求错误
        public const uint ME_OPE_EXECCHECK_SORT = (ME_OPE_EXECCHECK | ME_OPESRC_SORT); // 排序错误
        public const uint ME_OPE_EXECCHECK_NOSHAPE = (ME_OPE_EXECCHECK | 0x22); // 无形状错误
        public const uint ME_OPE_EXECCHECK_CTRLFLAG = (ME_OPE_EXECCHECK | 0x23); // 控制标志错误
        public const uint ME_OPE_EXECCHECK_CHECKING = (ME_OPE_EXECCHECK | ME_OPESRC_CHECKING); // 正在检查错误

        // 获取绘制数据相关操作代码
        public const uint ME_OPE_GETDRAWDATA = (ME_OPE_ERR | 0x1F00); // 获取绘制数据错误
        public const uint ME_OPE_GETDRAWDATA_RESET = (ME_OPE_GETDRAWDATA | ME_OPESRC_RESET); // 重置错误
        public const uint ME_OPE_GETDRAWDATA_TIMEOUT = (ME_OPE_GETDRAWDATA | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_GETDRAWDATA_ADDR = (ME_OPE_GETDRAWDATA | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_GETDRAWDATA_NOSEARCH = (ME_OPE_GETDRAWDATA | ME_OPESRC_TOPSEARCH); // 无搜索错误
        public const uint ME_OPE_GETDRAWDATA_NOTCHECK = (ME_OPE_GETDRAWDATA | 0x20); // 未检查错误
        public const uint ME_OPE_GETDRAWDATA_DATAERR = (ME_OPE_GETDRAWDATA | 0x21); // 数据错误

        // 设置键相关操作代码
        public const uint ME_OPE_SETKEY = (ME_ERR_FLG | 0x2000); // 设置键错误
        public const uint ME_OPE_SETKEY_DATA = (ME_OPE_SETKEY | ME_DATA_VALUE); // 数据错误

        // 仿真控制相关操作代码
        public const uint ME_OPE_SIMUCTRL = (ME_OPE_ERR | 0x2100); // 仿真控制错误
        public const uint ME_OPE_SIMUCTRL_ADDR = (ME_OPE_SIMUCTRL | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SIMUCTRL_NOOPTION = (ME_OPE_SIMUCTRL | 0x1); // 无选项错误
        public const uint ME_OPE_SIMUCTRL_MODE = (ME_OPE_SIMUCTRL | 0x2); // 模式错误
        public const uint ME_OPE_SIMUCTRL_FUNC = (ME_OPE_SIMUCTRL | 0x3); // 功能错误
        public const uint ME_OPE_SIMUCTRL_DATA = (ME_OPE_SIMUCTRL | 0x4); // 数据错误

        // 当前警报扩展相关操作代码
        public const uint ME_OPE_CURRALMEX = (ME_OPE_ERR | 0x2200); // 当前警报扩展错误
        public const uint ME_OPE_CURRALMEX_NOS = (ME_OPE_CURRALMEX | 0x1); // 无警报错误
        public const uint ME_OPE_CURRALMEX_ALMTYPE = (ME_OPE_CURRALMEX | 0x2); // 警报类型错误
        public const uint ME_OPE_CURRALMEX_DATAERR = (ME_OPE_CURRALMEX | 0x3); // 数据错误
        public const uint ME_OPE_CURRALMEX_NOALM = (ME_OPE_CURRALMEX | 0x4); // 无警报错误
        public const uint ME_OPE_CURRALMEX_ADDR = (ME_OPE_CURRALMEX | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_CURRALMEX_DATASIZE = (ME_OPE_CURRALMEX | ME_OPE_DATASIZE); // 数据大小错误
        public const uint ME_OPE_CURRALMEX_DATATYPE = (ME_OPE_CURRALMEX | ME_OPE_DATATYPE); // 数据类型错误

        // 仿真执行相关操作代码
        public const uint ME_OPE_SIMUEXEC = (ME_OPE_ERR | 0x2300); // 仿真执行错误
        public const uint ME_OPE_SIMUEXEC_NOTREGIST = (ME_OPE_SIMUEXEC | 0x1); // 未注册错误
        public const uint ME_OPE_SIMUEXEC_START = (ME_OPE_SIMUEXEC | 0x2); // 启动错误
        public const uint ME_OPE_SIMUEXEC_PARAM = (ME_OPE_SIMUEXEC | ME_OPE_PARAM); // 参数错误
        public const uint ME_OPE_SIMUEXEC_NOTSUPPORTED = (ME_OPE_SIMUEXEC | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误

        // 仿真停止相关操作代码
        public const uint ME_OPE_SIMUSTOP = (ME_OPE_ERR | 0x2400); // 仿真停止错误
        public const uint ME_OPE_SIMUSTOP_NOTREGIST = (ME_OPE_SIMUSTOP | 0x1); // 未注册错误
        public const uint ME_OPE_SIMUSTOP_PARAM = (ME_OPE_SIMUSTOP | ME_OPE_PARAM); // 参数错误
        public const uint ME_OPE_SIMUSTOP_NOTSUPPORTED = (ME_OPE_SIMUSTOP | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误

        // 选择程序采样时间相关操作代码
        public const uint ME_OPE_SELECT_PRCSTIME = (ME_OPE_ERR | 0x2500); // 选择程序采样时间错误
        public const uint ME_OPE_SELECT_PRCSTIME_ADDR = (ME_OPE_SELECT_PRCSTIME | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_SELECT_PRCSTIME_PRGFORMAT = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_PRGFORMAT); // 程序格式错误
        public const uint ME_OPE_SELECT_PRCSTIME_NOTPRG = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_NOPRG); // 无程序错误
        public const uint ME_OPE_SELECT_PRCSTIME_RUNNING = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_RUNNING); // 运行中错误
        public const uint ME_OPE_SELECT_PRCSTIME_FILESYSTEM = (ME_OPE_SELECT_PRCSTIME | ME_COMMON_FILESYSTEM); // 文件系统错误
        public const uint ME_OPE_SELECT_PRCSTIME_SEARCHING = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_SEARCHING); // 正在搜索错误
        public const uint ME_OPE_SELECT_PRCSTIME_CHECKING = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_CHECKING); // 正在检查错误
        public const uint ME_OPE_SELECT_PRCSTIME_TIMEOUT = (ME_OPE_SELECT_PRCSTIME | ME_OPESRC_TIMEOUT); // 超时错误
        public const uint ME_OPE_SELECT_PRCSTIME_NOTSUPPORTED = (ME_OPE_SELECT_PRCSTIME | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误


        // 获取程序采样时间相关操作代码
        public const uint ME_OPE_GET_PRCSTIME = (ME_OPE_ERR | 0x2600); // 获取程序采样时间错误
        public const uint ME_OPE_GET_PRCSTIME_ADDR = (ME_OPE_GET_PRCSTIME | ME_DATA_ADDR); // 地址错误
        public const uint ME_OPE_GET_PRCSTIME_DATASIZE = (ME_OPE_GET_PRCSTIME | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_OPE_GET_PRCSTIME_DATATYPE = (ME_OPE_GET_PRCSTIME | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_OPE_GET_PRCSTIME_PRGERR = (ME_OPE_GET_PRCSTIME | 0x20); // 程序错误
        public const uint ME_OPE_GET_PRCSTIME_RESET = (ME_OPE_GET_PRCSTIME | ME_OPESRC_RESET); // 重置错误

        // 重置程序采样时间相关操作代码
        public const uint ME_OPE_RESET_PRCSTIME = (ME_OPE_ERR | 0x2700); // 重置程序采样时间错误
        public const uint ME_OPE_RESET_PRCSTIME_ADDR = (ME_OPE_RESET_PRCSTIME | ME_OPE_ADDR); // 地址错误
        public const uint ME_OPE_RESET_PRCSTIME_CHECKING = (ME_OPE_RESET_PRCSTIME | ME_OPESRC_CHECKING); // 正在检查错误

        // 读取相关操作代码
        public const uint ME_READ = (0x60000); // 读取操作
        public const uint ME_READ_ERR = (ME_ERR_FLG | ME_READ); // 读取错误
        public const uint ME_READ_CACHE_ADDR = (ME_READ_ERR | ME_DATA_ADDR); // 地址错误
        public const uint ME_READ_CACHE_DATA = (ME_READ_ERR | ME_DATA_VALUE); // 数据错误
        public const uint ME_READ_CACHE_SECT = (ME_READ_ERR | ME_DATA_SECTION); // 节错误
        public const uint ME_READ_CACHE_SUBSECT = (ME_READ_ERR | ME_DATA_SUBSECTION); // 子节错误
        public const uint ME_READ_CACHE_AXIS = (ME_READ_ERR | ME_DATA_AXIS); // 轴错误
        public const uint ME_READ_CACHE_WRITEONLY = (ME_READ_ERR | ME_DATA_WRITEONLY); // 只写错误
        public const uint ME_READ_CACHE_READ = (ME_READ_ERR | ME_DATA_READERR); // 读取错误
        public const uint ME_READ_CACHE_DATATYPE = (ME_READ_ERR | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_READ_CACHE_REGIST = (ME_READ_ERR | 0x1); // 注册错误

        // 文件系统相关操作代码
        public const uint ME_FS_FILE = (0x70000); // 文件操作
        public const uint ME_FS_FILE_ERR = (ME_ERR_FLG | ME_FS_FILE); // 文件错误

        public const uint ME_FS_OPEN_FILE = (ME_FS_FILE_ERR | 0x100); // 打开文件操作
        public const uint ME_FS_OPEN_FILE_FILEFULL = (ME_FS_OPEN_FILE | ME_FILE_FILEFULL); // 文件已满错误
        public const uint ME_FS_OPEN_FILE_ALREADYOPEN = (ME_FS_OPEN_FILE | ME_FILE_OPEN); // 文件已打开错误
        public const uint ME_FS_OPEN_FILE_BUSY = (ME_FS_OPEN_FILE | ME_FILE_BUSY); // 文件忙错误
        public const uint ME_FS_OPEN_FILE_OPEN = (ME_FS_OPEN_FILE | ME_FILE_NOFILE); // 文件未找到错误
        public const uint ME_FS_OPEN_FILE_MALLOC = (ME_FS_OPEN_FILE | 0x40); // 内存分配错误
        public const uint ME_FS_OPEN_FILE_NOTSUPPORTED = (ME_FS_OPEN_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_OPEN_FILE_NODRIVE = (ME_FS_OPEN_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_OPEN_FILE_NAMELENGTH = (ME_FS_OPEN_FILE | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FS_OPEN_FILE_SORT = (ME_FS_OPEN_FILE | ME_FILE_SORT); // 排序错误
        public const uint ME_FS_OPEN_FILE_SAFE_NOPASSWD = (ME_FS_OPEN_FILE | ME_FILE_SAFE_NOPASSWD); // 无密码安全错误
        public const uint ME_FS_OPEN_FILE_PROTECT = (ME_FS_OPEN_FILE | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_FS_OPEN_FILE_WRITE_PROTECT = (ME_FS_OPEN_FILE | ME_FILE_WRITE_PROTECT); // 写保护错误
        public const uint ME_FS_OPEN_FILE_ENTRYOVER = (ME_FS_OPEN_FILE | ME_FILE_ENTRYOVER); // 条目溢出错误
        public const uint ME_FS_OPEN_FILE_ILLEGALNAME = (ME_FS_OPEN_FILE | ME_FILE_ILLEGALNAME); // 非法名称错误

        public const uint ME_FS_CLOSE_FILE = (ME_FS_FILE_ERR | 0x200); // 关闭文件操作
        public const uint ME_FS_CLOSE_FILE_NOTOPEN = (ME_FS_CLOSE_FILE | ME_FILE_NOTOPEN); // 文件未打开错误

        public const uint ME_FS_CREATE_FILE = (ME_FS_FILE_ERR | 0x300); // 创建文件操作
        public const uint ME_FS_CREATE_FILE_FILEFULL = (ME_FS_CREATE_FILE | ME_FILE_FILEFULL); // 文件已满错误
        public const uint ME_FS_CREATE_FILE_ALREADYOPEN = (ME_FS_CREATE_FILE | ME_FILE_OPEN); // 文件已打开错误
        public const uint ME_FS_CREATE_FILE_BUSY = (ME_FS_CREATE_FILE | ME_FILE_BUSY); // 文件忙错误
        public const uint ME_FS_CREATE_FILE_CREATE = (ME_FS_CREATE_FILE | ME_FILE_CREATE); // 创建错误
        public const uint ME_FS_CREATE_FILE_MALLOC = (ME_FS_CREATE_FILE | 0x40); // 内存分配错误
        public const uint ME_FS_CREATE_FILE_NOTSUPPORTED = (ME_FS_CREATE_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_CREATE_FILE_NODRIVE = (ME_FS_CREATE_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_CREATE_FILE_NAMELENGTH = (ME_FS_CREATE_FILE | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FS_CREATE_FILE_PROTECT = (ME_FS_CREATE_FILE | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_FS_CREATE_FILE_WRITE_PROTECT = (ME_FS_CREATE_FILE | ME_FILE_WRITE_PROTECT); // 写保护错误

        public const uint ME_FS_READ_FILE = (ME_FS_FILE_ERR | 0x400); // 读取文件操作
        public const uint ME_FS_READ_FILE_NOTOPEN = (ME_FS_READ_FILE | ME_FILE_NOTOPEN); // 文件未打开错误
        public const uint ME_FS_READ_FILE_READ = (ME_FS_READ_FILE | ME_FILE_READERR); // 读取错误
        public const uint ME_FS_READ_FILE_PROTECT = (ME_FS_READ_FILE | ME_COMMON_PROTECT); // 保护错误

        public const uint ME_FS_WRITE_FILE = (ME_FS_FILE_ERR | 0x500); // 写入文件操作
        public const uint ME_FS_WRITE_FILE_NOTOPEN = (ME_FS_WRITE_FILE | ME_FILE_NOTOPEN); // 文件未打开错误
        public const uint ME_FS_WRITE_FILE_WRITE = (ME_FS_WRITE_FILE | ME_FILE_WRITEERR); // 写入错误
        public const uint ME_FS_WRITE_FILE_NOTSUPPORTED = (ME_FS_WRITE_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_WRITE_FILE_PROTECT = (ME_FS_WRITE_FILE | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_FS_WRITE_FILE_SAFE_DATAERR = (ME_FS_WRITE_FILE | ME_FILE_SAFE_DATAERR); // 安全数据错误
        public const uint ME_FS_WRITE_FILE_SAFE_NOPASSWD = (ME_FS_WRITE_FILE | ME_FILE_SAFE_NOPASSWD); // 无密码安全错误
        public const uint ME_FS_WRITE_FILE_SAFE_PASSWDERR = (ME_FS_WRITE_FILE | ME_FILE_SAFE_PASSWDERR); // 密码错误
        public const uint ME_FS_WRITE_FILE_SAFE_CHECKERR = (ME_FS_WRITE_FILE | ME_FILE_SAFE_CHECKERR); // 检查错误
        public const uint ME_FS_WRITE_FILE_SEC_DISCREPANCY = (ME_FS_WRITE_FILE | ME_FILE_SEC_DISCREPANCY); // 安全差异错误
        public const uint ME_FS_WRITE_FILE_SEC_INVALID = (ME_FS_WRITE_FILE | ME_FILE_SEC_INVALID); // 安全无效错误
        public const uint ME_FS_WRITE_FILE_MEMORYOVER = (ME_FS_WRITE_FILE | ME_FILE_MEMORYOVER); // 内存溢出错误

        public const uint ME_FS_LSEEK_FILE = (ME_FS_FILE_ERR | 0x600); // 文件定位操作
        public const uint ME_FS_LSEEK_FILE_NOTOPEN = (ME_FS_LSEEK_FILE | ME_FILE_NOTOPEN); // 文件未打开错误
        public const uint ME_FS_LSEEK_FILE_FUNCTION = (ME_FS_LSEEK_FILE | ME_COMMON_CMDFORMAT); // 命令格式错误
        public const uint ME_FS_LSEEK_FILE_SEEKERR = (ME_FS_LSEEK_FILE | 0x40); // 定位错误


        // 移除文件相关操作代码
        public const uint ME_FS_REMOVE_FILE = (ME_FS_FILE_ERR | 0x700); // 移除文件错误
        public const uint ME_FS_REMOVE_FILE_ALREADYOPEN = (ME_FS_REMOVE_FILE | ME_FILE_OPEN); // 文件已打开错误
        public const uint ME_FS_REMOVE_FILE_BUSY = (ME_FS_REMOVE_FILE | ME_FILE_BUSY); // 文件忙错误
        public const uint ME_FS_REMOVE_FILE_NOFILE = (ME_FS_REMOVE_FILE | ME_FILE_NOFILE); // 文件未找到错误
        public const uint ME_FS_REMOVE_FILE_REMOVEERR = (ME_FS_REMOVE_FILE | 0x40); // 移除错误
        public const uint ME_FS_REMOVE_FILE_NOTSUPPORTED = (ME_FS_REMOVE_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_REMOVE_FILE_NODRIVE = (ME_FS_REMOVE_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_REMOVE_FILE_NAMELENGTH = (ME_FS_REMOVE_FILE | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FS_REMOVE_FILE_PROTECT = (ME_FS_REMOVE_FILE | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_FS_REMOVE_FILE_WRITE_PROTECT = (ME_FS_REMOVE_FILE | ME_FILE_WRITE_PROTECT); // 写保护错误

        // 重命名文件相关操作代码
        public const uint ME_FS_RENAME_FILE = (ME_FS_FILE_ERR | 0x800); // 重命名文件错误
        public const uint ME_FS_RENAME_FILE_NOFILE = (ME_FS_RENAME_FILE | ME_FILE_NOFILE); // 文件未找到错误
        public const uint ME_FS_RENAME_FILE_ALREADYOPEN = (ME_FS_RENAME_FILE | ME_FILE_OPEN); // 文件已打开错误
        public const uint ME_FS_RENAME_FILE_FILEFULL = (ME_FS_RENAME_FILE | ME_FILE_FILEFULL); // 文件已满错误
        public const uint ME_FS_RENAME_FILE_NOTRENAME = (ME_FS_RENAME_FILE | ME_COMMON_FILESYSTEM); // 无法重命名错误
        public const uint ME_FS_RENAME_FILE_NOTSUPPORTED = (ME_FS_RENAME_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_RENAME_FILE_NODRIVE = (ME_FS_RENAME_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_RENAME_FILE_NAMELENGTH = (ME_FS_RENAME_FILE | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FS_RENAME_FILE_PROTECT = (ME_FS_RENAME_FILE | ME_COMMON_PROTECT); // 保护错误
        public const uint ME_FS_RENAME_FILE_WRITE_PROTECT = (ME_FS_RENAME_FILE | ME_FILE_WRITE_PROTECT); // 写保护错误

        // IO控制文件相关操作代码
        public const uint ME_FS_IOCTL_FILE = (ME_FS_FILE_ERR | 0x900); // IO控制文件错误
        public const uint ME_FS_IOCTL_FILE_NOTOPEN = (ME_FS_IOCTL_FILE | ME_FILE_NOTOPEN); // 文件未打开错误
        public const uint ME_FS_IOCTL_FILE_READ = (ME_FS_IOCTL_FILE | ME_FILE_READERR); // 读取错误
        public const uint ME_FS_IOCTL_FILE_WRITE = (ME_FS_IOCTL_FILE | ME_FILE_WRITEERR); // 写入错误
        public const uint ME_FS_IOCTL_FILE_FUNCTION = (ME_FS_IOCTL_FILE | ME_COMMON_CMDFORMAT); // 命令格式错误
        public const uint ME_FS_IOCTL_FILE_NOTSUPPORTED = (ME_FS_IOCTL_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_IOCTL_FILE_DATATYPE = (ME_FS_IOCTL_FILE | ME_DATA_DATATYPE); // 数据类型错误
        public const uint ME_FS_IOCTL_FILE_DATASIZE = (ME_FS_IOCTL_FILE | ME_DATA_SIZE); // 数据大小错误
        public const uint ME_FS_IOCTL_FILE_VALUE = (ME_FS_IOCTL_FILE | ME_DATA_VALUE); // 数据值错误
        public const uint ME_FS_IOCTL_FILE_WRITE_PROTECT = (ME_FS_IOCTL_FILE | ME_FILE_WRITE_PROTECT); // 写保护错误

        // 打开目录相关操作代码
        public const uint ME_FS_OPEN_DIRECTORY = (ME_FS_FILE_ERR | 0xA00); // 打开目录错误
        public const uint ME_FS_OPEN_DIR_FILEFULL = (ME_FS_OPEN_DIRECTORY | ME_FILE_FILEFULL); // 目录已满错误
        public const uint ME_FS_OPEN_DIR_NOTOPEN = (ME_FS_OPEN_DIRECTORY | ME_FILE_OPEN); // 目录已打开错误
        public const uint ME_FS_OPEN_DIR_BUSY = (ME_FS_OPEN_DIRECTORY | ME_FILE_BUSY); // 目录忙错误
        public const uint ME_FS_OPEN_DIR_NODIR = (ME_FS_OPEN_DIRECTORY | ME_FILE_NODIR); // 无目录错误
        public const uint ME_FS_OPEN_DIR_MALLOC = (ME_FS_OPEN_DIRECTORY | 0x40); // 内存分配错误
        public const uint ME_FS_OPEN_DIR_NOTSUPPORTED = (ME_FS_OPEN_DIRECTORY | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_OPEN_DIR_NODRIVE = (ME_FS_OPEN_DIRECTORY | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_OPEN_DIR_NAMELENGTH = (ME_FS_OPEN_DIRECTORY | ME_FILE_NAMELENGTH); // 名称长度错误
        public const uint ME_FS_OPEN_DIR_WRITE_PROTECT = (ME_FS_OPEN_DIRECTORY | ME_FILE_WRITE_PROTECT); // 写保护错误

        // 读取目录相关操作代码
        public const uint ME_FS_READ_DIRECTORY = (ME_FS_FILE_ERR | 0xB00); // 读取目录错误
        public const uint ME_FS_READ_DIR_NOTOPEN = (ME_FS_READ_DIRECTORY | ME_FILE_NOTOPEN); // 目录未打开错误
        public const uint ME_FS_READ_DIR_NODIR = (ME_FS_READ_DIRECTORY | ME_FILE_NODIR); // 无目录错误
        public const uint ME_FS_READ_DIR_DATASIZE = (ME_FS_READ_DIRECTORY | ME_DATA_SIZE); // 数据大小错误

        // 回退目录相关操作代码
        public const uint ME_FS_REWIND_DIRECTORY = (ME_FS_FILE_ERR | 0xC00); // 回退目录错误
        public const uint ME_FS_REWIND_DIR_NOTOPEN = (ME_FS_REWIND_DIRECTORY | ME_FILE_NOTOPEN); // 目录未打开错误
        public const uint ME_FS_REWIND_DIR_REWINDERR = (ME_FS_REWIND_DIRECTORY | 0x40); // 回退错误

        // 关闭目录相关操作代码
        public const uint ME_FS_CLOSE_DIRECTORY = (ME_FS_FILE_ERR | 0xD00); // 关闭目录错误
        public const uint ME_FS_CLOSE_DIR_NOTOPEN = (ME_FS_CLOSE_DIRECTORY | ME_FILE_NOTOPEN); // 目录未打开错误

        // 状态文件相关操作代码
        public const uint ME_FS_STAT_FILE = (ME_FS_FILE_ERR | 0xE00); // 状态文件错误
        public const uint ME_FS_STAT_FILE_FILEFULL = (ME_FS_STAT_FILE | ME_FILE_FILEFULL); // 文件已满错误
        public const uint ME_FS_STAT_FILE_STATERR = (ME_FS_STAT_FILE | ME_FILE_READERR); // 状态读取错误
        public const uint ME_FS_STAT_FILE_NOTSUPPORTED = (ME_FS_STAT_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_STAT_FILE_NODRIVE = (ME_FS_STAT_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_STAT_FILE_NAMELENGTH = (ME_FS_STAT_FILE | ME_FILE_NAMELENGTH); // 名称长度错误

        // 文件状态相关操作代码
        public const uint ME_FS_FSTAT_FILE = (ME_FS_FILE_ERR | 0xF00); // 文件状态错误
        public const uint ME_FS_FSTAT_FILE_NOTOPEN = (ME_FS_FSTAT_FILE | ME_FILE_NOTOPEN); // 文件未打开错误
        public const uint ME_FS_FSTAT_FILE_STATERR = (ME_FS_FSTAT_FILE | ME_FILE_READERR); // 状态读取错误
        public const uint ME_FS_FSTAT_FILE_NOTSUPPORTED = (ME_FS_FSTAT_FILE | ME_COMMON_NOTSUPPORTED); // 不支持的操作错误
        public const uint ME_FS_FSTAT_FILE_NODRIVE = (ME_FS_FSTAT_FILE | ME_FILE_NODRIVE); // 无驱动器错误
        public const uint ME_FS_FSTAT_FILE_NAMELENGTH = (ME_FS_FSTAT_FILE | ME_FILE_NAMELENGTH); // 名称长度错误

        // 用户打开格式相关操作代码
        public const uint ME_FS_IOCTL_UOPEN_FORMAT = (ME_FS_IOCTL_FILE | ME_FILE_UOPEN_FORMAT); // 用户打开格式错误


        // FTP相关操作代码
        public const uint ME_FTP = 0x80000;
        public const uint ME_FTP_ERR = (ME_ERR_FLG | ME_FTP);
        public const uint ME_FTP_IOCTL = (ME_FTP_ERR | 0x1000); // FTP IO控制操作
        public const uint ME_FTP_UNKNOWNFUNC = (ME_FTP_ERR | ME_DEV_UNKNOWNFUNC); // 未知功能错误

        public const uint ME_FTP_SOCKET = (ME_FTP_IOCTL | 0x100); // 套接字错误
        public const uint ME_FTP_BIND = (ME_FTP_IOCTL | 0x200); // 绑定错误
        public const uint ME_FTP_LISTEN = (ME_FTP_IOCTL | 0x300); // 监听错误
        public const uint ME_FTP_ACCEPT = (ME_FTP_IOCTL | 0x400); // 接受连接错误
        public const uint ME_FTP_CLOSE = (ME_FTP_IOCTL | 0x500); // 关闭错误
        public const uint ME_FTP_CONNECT = (ME_FTP_IOCTL | 0x600); // 连接错误
        public const uint ME_FTP_SEND = (ME_FTP_IOCTL | 0x700); // 发送错误
        public const uint ME_FTP_RECV = (ME_FTP_IOCTL | 0x800); // 接收错误
        public const uint ME_FTP_GETSCKNAME = (ME_FTP_IOCTL | 0x900); // 获取套接字名称错误
        public const uint ME_FTP_HOSTNAME = (ME_FTP_IOCTL | 0xA00); // IP主机名错误

        public const uint ME_FTP_EACCES = 0x1; // 权限被拒绝错误
        public const uint ME_FTP_EAFNOSUPPORT = 0x2; // 地址族不支持错误
        public const uint ME_FTP_EINVAL = 0x3; // 无效参数错误
        public const uint ME_FTP_EMFILE = 0x4; // 打开的文件数过多错误
        public const uint ME_FTP_ENOMEM = 0x5; // 内存不足错误
        public const uint ME_FTP_EPROTONOSUPPORT = 0x6; // 协议不支持错误
        public const uint ME_FTP_EAGAIN = 0x7; // 资源暂时不可用错误
        public const uint ME_FTP_EBADF = 0x8; // 错误的文件描述符
        public const uint ME_FTP_ECONNREFUSED = 0x9; // 连接被拒绝错误
        public const uint ME_FTP_EFAULT = 0xA; // 错误的地址错误
        public const uint ME_FTP_EINTR = 0xB; // 被信号中断错误
        public const uint ME_FTP_ENOTCONN = 0xC; // 套接字未连接错误
        public const uint ME_FTP_ECONNRESET = 0xD; // 连接重置错误
        public const uint ME_FTP_EDESTADDRREQ = 0xE; // 目标地址请求错误
        public const uint ME_FTP_EISCONN = 0xF; // 套接字已连接错误
        public const uint ME_FTP_EMSGSIZE = 0x10; // 消息太大错误
        public const uint ME_FTP_EOPNOTSUPP = 0x11; // 操作不支持错误
        public const uint ME_FTP_EPIPE = 0x12; // 管道破裂错误
        public const uint ME_FTP_EADDRINUSE = 0x13; // 地址已使用错误
        public const uint ME_FTP_UNKNOWN = 0xFF; // 未知错误

        // NC系统相关操作代码
        public const uint ME_SYS = 0x1000000;
        public const uint ME_SYS_ERR = (ME_ERR_FLG | ME_SYS); // NC系统错误

        public const uint ME_SYS_CONTROLER = (ME_SYS_ERR | 0x100);
        public const uint ME_SYS_READYON = (ME_SYS_CONTROLER | 0x1); // 系统准备就绪
        public const uint ME_SYS_SYSTEMDOWN = (ME_SYS_CONTROLER | 0x2); // 系统关闭错误

        public const uint ME_SYS_OS = (ME_SYS_ERR | 0x8000);
        public const uint ME_SYS_MALLOC = (ME_SYS_OS | 0x1); // 内存分配错误

        // PC系统相关操作代码
        public const uint ME_PCSYS = 0x1010000;
        public const uint ME_PCSYS_ERR = (ME_ERR_FLG | ME_PCSYS); // PC系统错误

        public const uint ME_PCSYS_WIN = (ME_PCSYS_ERR | 0x100);
        public const uint ME_PCSYS_EXECCOMTASK = (ME_PCSYS_WIN | 0x1); // 执行命令任务错误
        public const uint ME_PCSYS_SEMAPHRETIMEOUT = (ME_PCSYS_WIN | ME_DEV_TIMEOUT); // 信号量超时错误

        public const uint ME_PCSYS_OS = (ME_PCSYS_ERR | 0x8000);
        public const uint ME_PCSYS_MALLOC = (ME_PCSYS_OS | 0x1); // 内存分配错误
        public const uint ME_PCSYS_MLOCK = (ME_PCSYS_OS | 0x2); // 内存锁定错误
        public const uint ME_PCSYS_SEMAPHRE = (ME_PCSYS_OS | 0x3); // 信号量错误
        public const uint ME_PCSYS_TRANSLATION = (ME_PCSYS_OS | 0x4); // 翻译错误
        public const uint ME_PCSYS_VALUE = (ME_PCSYS_OS | ME_DATA_VALUE); // 数据值错误

        // 共享内存相关操作代码
        public const uint ME_SMEM = 0x2000000;
        public const uint ME_SMEM_ERR = (ME_ERR_FLG | ME_SMEM); // 共享内存错误

        public const uint ME_SMEM_ALREADYOPEN = (ME_SMEM_ERR | ME_DEV_ALREADYOPEN); // 已经打开错误
        public const uint ME_SMEM_NOTOPEN = (ME_SMEM_ERR | ME_DEV_NOTOPEN); // 未打开错误
        public const uint ME_SMEM_CARDNOTEXIST = (ME_SMEM_ERR | ME_DEV_CARDNOTEXIST); // 卡不存在错误
        public const uint ME_SMEM_BADCHANNEL = (ME_SMEM_ERR | ME_DEV_BADCHANNEL); // 错误的通道错误
        public const uint ME_SMEM_BADFD = (ME_SMEM_ERR | ME_DEV_BADFD); // 错误的文件描述符错误
        public const uint ME_SMEM_CANNOTOPEN = (ME_SMEM_ERR | ME_DEV_CANNOTOPEN); // 无法打开错误
        public const uint ME_SMEM_TIMEOUT = (ME_SMEM_ERR | ME_DEV_TIMEOUT); // 超时错误
        public const uint ME_SMEM_DATAERR = (ME_SMEM_ERR | ME_DEV_DATAERR); // 数据错误
        public const uint ME_SMEM_CANCELED = (ME_SMEM_ERR | ME_DEV_CANCELED); // 已取消错误
        public const uint ME_SMEM_ILLEGALSIZE = (ME_SMEM_ERR | ME_DEV_ILLEGALSIZE); // 非法大小错误
        public const uint ME_SMEM_TASKQUIT = (ME_SMEM_ERR | ME_DEV_TASKQUIT); // 任务退出错误
        public const uint ME_SMEM_UNKNOWNFUNC = (ME_SMEM_ERR | ME_DEV_UNKNOWNFUNC); // 未知功能错误
        public const uint ME_SMEM_SETDATAERR = (ME_SMEM_ERR | ME_DEV_SETDATAERR); // 设置数据错误
                                                                                 // SIO相关错误代码
        public const uint ME_SIO = 0x2010000;
        public const uint ME_SIO_ERR = (ME_ERR_FLG | ME_SIO); // SIO错误
        public const uint ME_SIO_ERR_IO = (ME_SIO_ERR | 0x1); // I/O错误
        public const uint ME_SIO_ERR_HANDLER = (ME_SIO_ERR | 0x2); // 处理程序错误
        public const uint ME_SIO_ERR_OPENED = (ME_SIO_ERR | 0xA); // 已经打开错误
        public const uint ME_SIO_ERR_PARAMETER = (ME_SIO_ERR | 0xE); // 参数错误
        public const uint ME_SIO_ERR_NOT_READY = (ME_SIO_ERR | 0x10); // 端口未准备好
        public const uint ME_SIO_ERR_TIMEOUT = (ME_SIO_ERR | 0x1E); // 超时错误
        public const uint ME_SIO_ERR_OVER_RUN = (ME_SIO_ERR | 0x20); // 数据溢出错误
        public const uint ME_SIO_ERR_PARITY = (ME_SIO_ERR | 0x21); // 奇偶校验错误
        public const uint ME_SIO_ERR_PARITYV = (ME_SIO_ERR | 0x22); // 奇偶校验值错误
        public const uint ME_SIO_ERR_UNKNOWN = (ME_SIO_ERR | 0xFF); // 未知错误

        // ENET相关错误代码
        public const uint ME_ENET = 0x2020000;
        public const uint ME_ENET_ERR = (ME_ERR_FLG | ME_ENET);

        public const uint ME_ENET_ALREADYOPEN = (ME_ENET_ERR | ME_DEV_ALREADYOPEN); // 已经打开错误
        public const uint ME_ENET_NOTOPEN = (ME_ENET_ERR | ME_DEV_NOTOPEN); // 未打开错误
        public const uint ME_ENET_CARDNOTEXIST = (ME_ENET_ERR | ME_DEV_CARDNOTEXIST); // 卡不存在错误
        public const uint ME_ENET_BADCHANNEL = (ME_ENET_ERR | ME_DEV_BADCHANNEL); // 错误的通道错误
        public const uint ME_ENET_BADFD = (ME_ENET_ERR | ME_DEV_BADFD); // 错误的文件描述符错误
        public const uint ME_ENET_NOTCONNECT = (ME_ENET_ERR | ME_DEV_NOTCONNECT); // 未连接错误
        public const uint ME_ENET_NOTCLOSE = (ME_ENET_ERR | ME_DEV_NOTCLOSE); // 未关闭错误
        public const uint ME_ENET_TIMEOUT = (ME_ENET_ERR | ME_DEV_TIMEOUT); // 超时错误
        public const uint ME_ENET_DATAERR = (ME_ENET_ERR | ME_DEV_DATAERR); // 数据错误
        public const uint ME_ENET_CANCELED = (ME_ENET_ERR | ME_DEV_CANCELED); // 已取消错误
        public const uint ME_ENET_ILLEGALSIZE = (ME_ENET_ERR | ME_DEV_ILLEGALSIZE); // 非法大小错误
        public const uint ME_ENET_TASKQUIT = (ME_ENET_ERR | ME_DEV_TASKQUIT); // 任务退出错误
        public const uint ME_ENET_UNKNOWNFUNC = (ME_ENET_ERR | ME_DEV_UNKNOWNFUNC); // 未知功能错误
        public const uint ME_ENET_SETDATAERR = (ME_ENET_ERR | ME_DEV_SETDATAERR); // 设置数据错误

        // EasySocketPC相关错误代码
        public const uint ME_EZPC = 0x2040000;
        public const uint ME_EZPC_ERR = (ME_ERR_FLG | ME_EZPC); // EasySocketPC错误
        public const uint ME_EZPC_OBJCTFREE = (ME_EZPC_ERR | 0x1); // EasySocketPC对象释放错误

        // 通用状态
        public const uint ME_X = 0xFFF0000;
        public const uint ME_STS_OK = 0; // 状态正常
        public const int ME_STS_ERR = -1; // 状态错误


        // 内存分配相关错误代码
        public const uint EZ_ERR_MEMORY_ALLOC = 0x80B00101; // 内存分配错误
        public const uint EZNC_ERR_MEMORY_ALLOC = 0x80B00101; // 内存分配错误
        public const uint EZNC_ERR_CANNOT_GETPCERR = 0x80B00102; // EZSocketPC错误获取失败

        // 文件打开相关错误代码
        public const uint EZNC_FILE_OPEN_MODE = 0x80B00201; // 文件打开模式错误
        public const uint EZNC_FILE_OPEN_NOTOPEN = 0x80B00202; // 文件未打开
        public const uint EZNC_FILE_OPEN_FILEEXIST = 0x80B00203; // 文件已存在
        public const uint EZNC_FILE_OPEN_ALREADYOPENED = 0x80B00204; // 文件已经打开
        public const uint EZNC_FILE_OPEN_CREATE = 0x80B00205; // 创建文件错误
        public const uint EZNC_FILE_WRITEFILE_NOTOPEN = 0x80B00206; // 写入文件时未打开
        public const uint EZNC_FILE_WRITEFILE_LENGTH = 0x80B00207; // 写入文件长度错误
        public const uint EZNC_FILE_WRITEFILE_WRITE = 0x80B00208; // 写入文件错误
        public const uint EZNC_FILE_READFILE_NOTOPEN = 0x80B00209; // 读取文件时未打开
        public const uint EZNC_FILE_READFILE_READ = 0x80B0020A; // 读取文件错误
        public const uint EZNC_FILE_READFILE_CREATE = 0x80B0020B; // 创建文件错误
        public const uint EZNC_FILE_OPEN_FILENOTEXIST = 0x80B0020C; // 文件不存在
        public const uint EZNC_FILE_OPEN_OPEN = 0x80B0020D; // 打开文件错误
        public const uint EZNC_FILE_OPEN_ILLEGALPATH = 0x80B0020E; // 文件路径非法
        public const uint EZNC_FILE_READFILE_ILLEGALFILE = 0x80B0020F; // 读取的文件非法
        public const uint EZNC_FILE_WRITEFILE_ILLEGALFILE = 0x80B00210; // 写入的文件非法

        // 通信相关错误代码
        public const uint EZNC_COMM_CANNOT_OPEN = 0x80B00301; // 无法打开通信
        public const uint EZNC_COMM_NOTSETUP_PROTOCOL = 0x80B00302; // TCP/IP协议未设置
        public const uint EZNC_COMM_ALREADYOPENED = 0x80B00303; // 通信已打开
        public const uint EZNC_COMM_NOTMODULE = 0x80B00304; // 模块不存在
        public const uint EZNC_COMM_CREATEPC = 0x80B00305; // 创建EZSocketPC失败

        // 数据相关错误代码
        public const uint EZNC_DATA_NOT_EXIST = 0x80B00401; // 数据不存在
        public const uint EZNC_DATA_DUPLICATE = 0x80B00402; // 数据重复

        // 参数相关错误代码
        public const uint EZNC_PARAM_FILENOTEXIST = 0x80B00501; // 文件不存在错误

        // 系统功能相关错误代码
        public const uint EZNC_SYSFUNC_IOCTL_ADDR = ME_SYSFUNC_IOCTL_ADDR; // NC功能地址错误
        public const uint EZNC_SYSFUNC_IOCTL_NOTOPEN = ME_SYSFUNC_IOCTL_NOTOPEN; // 功能未打开
        public const uint EZNC_SYSFUNC_IOCTL_DATA = ME_SYSFUNC_IOCTL_DATA; // 数据错误
        public const uint EZNC_SYSFUNC_IOCTL_FUNCTION = ME_SYSFUNC_IOCTL_FUNCTION; // 功能错误

        // 操作相关错误代码
        public const uint EZNC_OPE_ACTPLC_ADDR = ME_OPE_ACTPLC_ADDR; // 地址错误
        public const uint EZNC_OPE_ACTPLC_MODE = ME_OPE_ACTPLC_MODE; // 模式错误

        public const uint EZNC_OPE_CURRALM_ADDR = ME_OPE_CURRALM_ADDR; // 地址错误
        public const uint EZNC_OPE_CURRALM_ALMTYPE = ME_OPE_CURRALM_ALMTYPE; // 报警类型错误
        public const uint EZNC_OPE_CURRALM_DATAERR = ME_OPE_CURRALM_DATAERR; // 数据错误
        public const uint EZNC_OPE_CURRALM_DATASIZE = ME_OPE_CURRALM_DATASIZE; // 数据大小错误
        public const uint EZNC_OPE_CURRALM_NOS = ME_OPE_CURRALM_NOS; // 没有报警
        public const uint EZNC_OPE_CURRALM_DATATYPE = ME_OPE_CURRALM_DATATYPE; // 数据类型错误

        public const uint EZNC_OPE_CURRALMEX_NOS = ME_OPE_CURRALMEX_NOS; // 没有报警
        public const uint EZNC_OPE_CURRALMEX_ALMTYPE = ME_OPE_CURRALMEX_ALMTYPE; // 报警类型错误
        public const uint EZNC_OPE_CURRALMEX_DATAERR = ME_OPE_CURRALMEX_DATAERR; // 数据错误
        public const uint EZNC_OPE_CURRALMEX_ADDR = ME_OPE_CURRALMEX_ADDR; // 地址错误
        public const uint EZNC_OPE_CURRALMEX_DATASIZE = ME_OPE_CURRALMEX_DATASIZE; // 数据大小错误
        public const uint EZNC_OPE_CURRALMEX_DATATYPE = ME_OPE_CURRALMEX_DATATYPE; // 数据类型错误

        public const uint EZNC_OPE_GETPRGBLK_NOS = ME_OPE_GETPRGBLK_NOS; // 没有程序块
        public const uint EZNC_OPE_GETPRGBLK_ADDR = ME_OPE_GETPRGBLK_ADDR; // 地址错误
        public const uint EZNC_OPE_GETPRGBLK_NOSEARCH = ME_OPE_GETPRGBLK_NOSEARCH; // 不搜索
        public const uint EZNC_OPE_GETPRGBLK_DATAERR = ME_OPE_GETPRGBLK_DATAERR; // 数据错误
        public const uint EZNC_OPE_GETPRGBLK_DATASIZE = ME_OPE_GETPRGBLK_DATASIZE; // 数据大小错误
        public const uint EZNC_OPE_GETPRGBLK_DATATYPE = ME_OPE_GETPRGBLK_DATATYPE; // 数据类型错误

        public const uint EZNC_OPE_SELECTPRG_ADDR = ME_OPE_SELECTPRG_ADDR; // 地址错误
        public const uint EZNC_OPE_SELECTPRG_FILESYSTEM = ME_OPE_SELECTPRG_FILESYSTEM; // 文件系统错误
        public const uint EZNC_OPE_SELECTPRG_NOPRG = ME_OPE_SELECTPRG_NOPRG; // 没有程序
        public const uint EZNC_OPE_SELECTPRG_PRGFORMAT = ME_OPE_SELECTPRG_PRGFORMAT; // 程序格式错误
        public const uint EZNC_OPE_SELECTPRG_RUNNING = ME_OPE_SELECTPRG_RUNNING; // 程序正在运行
        public const uint EZNC_OPE_SELECTPRG_RESET = ME_OPE_SELECTPRG_RESET; // 重置错误
        public const uint EZNC_OPE_SELECTPRG_LONGPATH = ME_OPE_SELECTPRG_LONGPATH; // 路径过长
        public const uint EZNC_OPE_SELECTPRG_TIMEOUT = ME_OPE_SELECTPRG_TIMEOUT; // 超时错误
        public const uint EZNC_OPE_SELECTPRG_FILEREAD = ME_OPE_SELECTPRG_FILEREAD; // 文件读取错误
        public const uint EZNC_OPE_SELECTPRG_FILEWRITE = ME_OPE_SELECTPRG_FILEWRITE; // 文件写入错误
        public const uint EZNC_OPE_SELECTPRG_DATATYPE = ME_OPE_SELECTPRG_DATATYPE; // 数据类型错误
        public const uint EZNC_OPE_SELECTPRG_NOTPRG = ME_OPE_SELECTPRG_NOTPRG; // 不是程序
        public const uint EZNC_OPE_SELECTPRG_NCPCCOM = ME_OPE_SELECTPRG_NCPCCOM; // ncpccom.exe错误
        public const uint EZNC_OPE_SELECTPRG_SEARCHING = ME_OPE_SELECTPRG_SEARCHING; // 正在搜索
        public const uint EZNC_OPE_SELECTPRG_CHECKING = ME_OPE_SELECTPRG_CHECKING; // 正在检查
        public const uint EZNC_OPE_SELECTPRG_MODE = ME_OPE_SELECTPRG_MODE; // 模式错误
        public const uint EZNC_OPE_SELECTPRG_NOTSUPPORTED = ME_OPE_SELECTPRG_NOTSUPPORTED; // 不支持
        public const uint EZNC_OPE_SELECTPRG_PGLKC = ME_OPE_SELECTPRG_PGLKC; // 平台锁定错误
        public const uint EZNC_OPE_SELECTPRG_ILLEGALPOS = ME_OPE_SELECTPRG_ILLEGALPOS; // 位置非法
        public const uint EZNC_OPE_SELECTPRG_REVERSE = ME_OPE_SELECTPRG_REVERSE; // 反转错误
        public const uint EZNC_OPE_SELECTPRG_PROTECT = ME_OPE_SELECTPRG_PROTECT; // 保护错误

        // 数据读取相关错误代码
        public const uint EZNC_DATA_READ_ADDR = ME_DATA_READ_ADDR; // 地址错误
        public const uint EZNC_DATA_READ_DATASIZE = ME_DATA_READ_DATASIZE; // 数据大小错误
        public const uint EZNC_DATA_READ_READ = ME_DATA_READ_READ; // 读取错误
        public const uint EZNC_DATA_READ_DATATYPE = ME_DATA_READ_DATATYPE; // 数据类型错误
        public const uint EZNC_DATA_READ_SECT = ME_DATA_READ_SECT; // 段错误
        public const uint EZNC_DATA_READ_SUBSECT = ME_DATA_READ_SUBSECT; // 子段错误
        public const uint EZNC_DATA_READ_WRITEONLY = ME_DATA_READ_WRITEONLY; // 只读错误
        public const uint EZNC_DATA_READ_AXIS = ME_DATA_READ_AXIS; // 轴错误
        public const uint EZNC_DATA_READ_DATANUM = ME_DATA_READ_DATANUM; // 数据数量错误
        public const uint EZNC_DATA_READ_NODATA = ME_DATA_READ_NODATA; // 无数据错误
        public const uint EZNC_DATA_READ_VALUE = ME_DATA_READ_VALUE; // 值读取错误


        // 数据写入相关错误代码
        public const uint EZNC_DATA_WRITE_ADDR = ME_DATA_WRITE_ADDR;                          // 地址错误
        public const uint EZNC_DATA_WRITE_DATASIZE = ME_DATA_WRITE_DATASIZE;                  // 数据大小错误
        public const uint EZNC_DATA_WRITE_WRITE = ME_DATA_WRITE_WRITE;                        // 写入错误
        public const uint EZNC_DATA_WRITE_DATATYPE = ME_DATA_WRITE_DATATYPE;                  // 数据类型错误
        public const uint EZNC_DATA_WRITE_SECT = ME_DATA_WRITE_SECT;                          // 段错误
        public const uint EZNC_DATA_WRITE_SUBSECT = ME_DATA_WRITE_SUBSECT;                    // 子段错误
        public const uint EZNC_DATA_WRITE_READONLY = ME_DATA_WRITE_READONLY;                  // 只读错误
        public const uint EZNC_DATA_WRITE_AXIS = ME_DATA_WRITE_AXIS;                          // 轴错误
        public const uint EZNCDATA_WRITE_SAFETYPWLOCK = ME_DATA_WRITE_SAFETYPWLOCK;           // 安全密码锁定错误
        public const uint EZNCDATA_WRITE_UOPEN_FORMAT = ME_DATA_WRITE_UOPEN_FORMAT;           // 格式错误
        public const uint EZNCDATA_WRITE_EDTFILE_REGIST = ME_DATA_WRITE_EDTFILE_REGIST;       // 注册文件错误
        public const uint EZNCDATA_WRITE_EDTFILE_RELEASE = ME_DATA_WRITE_EDTFILE_RELEASE;     // 释放文件错误
        public const uint EZNCDATA_WRITE_NODATA = ME_DATA_WRITE_NODATA;                       // 无数据错误
        public const uint EZNCDATA_WRITE_VALUE = ME_DATA_WRITE_VALUE;                         // 值错误
        public const uint EZNCDATA_WRITE_SAFE_NOPASSWD = ME_DATA_WRITE_SAFE_NOPASSWD;         // 无密码安全错误
        public const uint EZNCDATA_WRITE_SAFE_CHECKERR = ME_DATA_WRITE_SAFE_CHECKERR;         // 安全检查错误
        public const uint EZNCDATA_WRITE_SAFE_DATATYPE = ME_DATA_WRITE_SAFE_DATATYPE;         // 安全数据类型错误
        public const uint EZNCDATA_WRITE_SORT = ME_DATA_WRITE_SORT;                           // 排序错误

        // 模块取消注册相关错误代码
        public const uint EZNC_DATA_MDLCANCEL_NOTREGIST = ME_DATA_MDLCANCEL_NOTREGIST;        // 未注册错误

        // 模块注册相关错误代码
        public const uint EZNC_DATA_MDLREGIST_PRIORITY = ME_DATA_MDLREGIST_PRIORITY;          // 优先级错误
        public const uint EZNC_DATA_MDLREGIST_REGIST = ME_DATA_MDLREGIST_REGIST;              // 注册错误
        public const uint EZNC_DATA_MDLREGIST_ADDR = ME_DATA_MDLREGIST_ADDR;                  // 地址错误
        public const uint EZNC_DATA_MDLREGIST_SECT = ME_DATA_MDLREGIST_SECT;                  // 段错误
        public const uint EZNC_DATA_MDLREGIST_SUBSECT = ME_DATA_MDLREGIST_SUBSECT;            // 子段错误
        public const uint EZNC_DATA_MDLREGIST_AXIS = ME_DATA_MDLREGIST_AXIS;                  // 轴错误
        public const uint EZNC_DATA_MDLREGIST_WRITEONLY = ME_DATA_MDLREGIST_WRITEONLY;        // 只写错误
        public const uint EZNC_DATA_MDLREGIST_READONLY = ME_DATA_MDLREGIST_READONLY;          // 只读错误
        public const uint EZNC_DATA_MDLREGIST_DATATYPE = ME_DATA_MDLREGIST_DATATYPE;          // 数据类型错误
        public const uint EZNC_DATA_MDLREGIST_READ = ME_DATA_MDLREGIST_READ;                  // 读取错误

        // 重线程写入相关错误代码
        public const uint EZNC_DATA_RETHREADWRITE_NODATA = ME_DATA_RETHREADWRITE_NODATA;      // 无数据错误

        // 组数据相关错误代码
        public const uint EZNC_DATA_TLFGROUP_ADDR = ME_DATA_TLFGROUP_ADDR;                    // 地址错误
        public const uint EZNC_DATA_TLFGROUP_EXIST = ME_DATA_TLFGROUP_EXIST;                  // 组存在错误
        public const uint EZNC_DATA_TLFGROUP_NONEXIST = ME_DATA_TLFGROUP_NONEXIST;            // 组不存在错误
        public const uint EZNC_DATA_TLFGROUP_OVER = ME_DATA_TLFGROUP_OVER;                    // 组溢出错误
        public const uint EZNC_DATA_TLFGROUP_NONFORMAT = ME_DATA_TLFGROUP_NONFORMAT;          // 非格式错误
        public const uint EZNC_DATA_TLFGROUP_UNMACH = ME_DATA_TLFGROUP_UNMACH;                // 格式不匹配错误
        public const uint EZNC_DATA_TLFGROUP_OUTOFSPEC = ME_DATA_TLFGROUP_OUTOFSPEC;          // 超出规格错误

        // 工具数据相关错误代码
        public const uint EZNC_DATA_TLFTOOL_ADDR = ME_DATA_TLFTOOL_ADDR;                      // 地址错误
        public const uint EZNC_DATA_TLFTOOL_EXIST = ME_DATA_TLFTOOL_EXIST;                    // 工具存在错误
        public const uint EZNC_DATA_TLFTOOL_NONEXIST = ME_DATA_TLFTOOL_NONEXIST;              // 工具不存在错误
        public const uint EZNC_DATA_TLFTOOL_OVER = ME_DATA_TLFTOOL_OVER;                      // 工具溢出错误
        public const uint EZNC_DATA_TLFTOOL_PARAMERR = ME_DATA_TLFTOOL_PARAMERR;              // 参数错误
        public const uint EZNC_DATA_TLFTOOL_MAXMINERR = ME_DATA_TLFTOOL_MAXMINERR;            // 最大最小值错误
        public const uint EZNC_DATA_TLFTOOL_UNMACH = ME_DATA_TLFTOOL_UNMACH;                  // 格式不匹配错误
        public const uint EZNC_DATA_TLFTOOL_OUTOFSPEC = ME_DATA_TLFTOOL_OUTOFSPEC;            // 超出规格错误

        // 文件目录相关错误代码
        public const uint EZNC_FILE_DIR_ALREADYOPENED = ME_FILE_DIR_ALREADYOPENED;            // 目录已打开
        public const uint EZNC_FILE_DIR_DATASIZE = ME_FILE_DIR_DATASIZE;                      // 数据大小错误
        public const uint EZNC_FILE_DIR_NOTOPEN = ME_FILE_DIR_NOTOPEN;                        // 目录未打开
        public const uint EZNC_FILE_DIR_READ = ME_FILE_DIR_READ;                              // 读取错误
        public const uint EZNC_FILE_DIR_FILESYSTEM = ME_FILE_DIR_FILESYSTEM;                  // 文件系统错误
        public const uint EZNC_FILE_DIR_NODIR = ME_FILE_DIR_NODIR;                            // 目录不存在错误
        public const uint EZNC_FILE_DIR_NODRIVE = ME_FILE_DIR_NODRIVE;                        // 驱动器不存在错误
        public const uint EZNC_FILE_DIR_NAMELENGTH = ME_FILE_DIR_NAMELENGTH;                  // 名称长度错误
        public const uint EZNC_FILE_DIR_ILLEGALNAME = ME_FILE_DIR_ILLEGALNAME;                // 非法名称错误
        public const uint EZNC_PCFILE_DIR_NODIR = ME_PCFILE_DIR_NODIR;                        // 目录不存在错误
        public const uint EZNC_PCFILE_DIR_NOFILE = ME_PCFILE_DIR_NOFILE;                      // 文件不存在错误
        public const uint EZNC_PCFILE_DIR_NODRIVE = ME_PCFILE_DIR_NODRIVE;                    // 驱动器不存在错误
        public const uint EZNC_PCFILE_DIR_NOTOPEN = ME_PCFILE_DIR_NOTOPEN;                    // 目录未打开错误
        public const uint EZNC_PCFILE_DIR_READ = ME_PCFILE_DIR_READ;                          // 读取错误
        public const uint EZNC_PCFILE_DIR_ALREADYOPENED = ME_PCFILE_DIR_ALREADYOPENED;        // PC目录已打开

        // 文件复制相关错误代码
        public const uint EZNC_FILE_COPY_BUSY = ME_FILE_COPY_BUSY;                            // 文件忙
        public const uint EZNC_FILE_COPY_ENTRYOVER = ME_FILE_COPY_ENTRYOVER;                  // 条目溢出错误
        public const uint EZNC_FILE_COPY_FILEEXIST = ME_FILE_COPY_FILEEXIST;                  // 文件已存在
        public const uint EZNC_FILE_COPY_FILESYSTEM = ME_FILE_COPY_FILESYSTEM;                // 文件系统错误
        public const uint EZNC_FILE_COPY_ILLEGALNAME = ME_FILE_COPY_ILLEGALNAME;              // 非法名称错误
        public const uint EZNC_FILE_COPY_MEMORYOVER = ME_FILE_COPY_MEMORYOVER;                // 内存溢出错误
        public const uint EZNC_FILE_COPY_NAMELENGTH = ME_FILE_COPY_NAMELENGTH;                // 名称长度错误
        public const uint EZNC_FILE_COPY_PROTECT = ME_FILE_COPY_PROTECT;                      // 保护错误
        public const uint EZNC_FILE_COPY_NODIR = ME_FILE_COPY_NODIR;                          // 目录不存在错误
        public const uint EZNC_FILE_COPY_NODRIVE = ME_FILE_COPY_NODRIVE;                      // 驱动器不存在错误
        public const uint EZNC_FILE_COPY_NOFILE = ME_FILE_COPY_NOFILE;                        // 文件不存在错误
        public const uint EZNC_FILE_COPY_PLCRUN = ME_FILE_COPY_PLCRUN;                        // PLC运行错误
        public const uint EZNC_FILE_COPY_READ = ME_FILE_COPY_READ;                            // 读取错误
        public const uint EZNC_FILE_COPY_WRITE = ME_FILE_COPY_WRITE;                          // 写入错误
        public const uint EZNC_FILE_COPY_WRITE_WARNING = ME_FILE_COPY_WRITE_WARNING;          // 写入警告
        public const uint EZNC_FILE_COPY_DIFFER = ME_FILE_COPY_DIFFER;                        // 不同错误
        public const uint EZNC_FILE_COPY_NOTSUPPORTED = ME_FILE_COPY_NOTSUPPORTED;            // 不支持错误
        public const uint EZNC_FILE_COPY_NOTOPEN = ME_FILE_COPY_NOTOPEN;                      // 文件未打开
        public const uint EZNC_FILE_COPY_EXECUTING = ME_FILE_COPY_EXECUTING;                  // 执行中错误
        public const uint EZNC_FILE_COPY_SAFETYPWLOCK = ME_FILE_COPY_SAFETYPWLOCK;            // 安全密码锁定错误
        public const uint EZNC_FILE_COPY_ILLEGALFORMAT = ME_FILE_COPY_ILLEGALFORMAT;          // 非法格式错误
        public const uint EZNC_FILE_COPY_WRONGPASSWORD = ME_FILE_COPY_WRONGPASSWORD;          // 密码错误
        public const uint EZNC_PCFILE_COPY_CREATE = ME_PCFILE_COPY_CREATE;                    // 创建错误
        public const uint EZNC_PCFILE_COPY_OPEN = ME_PCFILE_COPY_OPEN;                        // 打开错误
        public const uint EZNC_PCFILE_COPY_FILEEXIST = ME_PCFILE_COPY_FILEEXIST;              // 文件已存在
        public const uint EZNC_PCFILE_COPY_ILLEGALNAME = ME_PCFILE_COPY_ILLEGALNAME;          // 非法名称错误
        public const uint EZNC_PCFILE_COPY_NODIR = ME_PCFILE_COPY_NODIR;                      // 目录不存在错误
        public const uint EZNC_PCFILE_COPY_NODRIVE = ME_PCFILE_COPY_NODRIVE;                  // 驱动器不存在错误
        public const uint EZNC_PCFILE_COPY_NOFILE = ME_PCFILE_COPY_NOFILE;                    // 文件不存在错误
        public const uint EZNC_PCFILE_COPY_READ = ME_PCFILE_COPY_READ;                        // 读取错误
        public const uint EZNC_PCFILE_COPY_WRITE = ME_PCFILE_COPY_WRITE;                      // 写入错误
        public const uint EZNC_PCFILE_COPY_NOTOPEN = ME_PCFILE_COPY_NOTOPEN;                  // 文件未打开
        public const uint EZNC_PCFILE_COPY_MEMORYOVER = ME_PCFILE_COPY_MEMORYOVER;            // 内存溢出错误

        // 文件删除相关错误代码
        public const uint EZNC_FILE_DEL_BUSY = ME_FILE_DEL_BUSY;                        // 文件忙
        public const uint EZNC_FILE_DEL_NOTDELETE = ME_FILE_DEL_NOTDELETE;              // 文件未删除
        public const uint EZNC_FILE_DEL_FILESYSTEM = ME_FILE_DEL_FILESYSTEM;            // 文件系统错误
        public const uint EZNC_FILE_DEL_ILLEGALNAME = ME_FILE_DEL_ILLEGALNAME;          // 非法名称错误
        public const uint EZNC_FILE_DEL_NODIR = ME_FILE_DEL_NODIR;                      // 目录不存在
        public const uint EZNC_FILE_DEL_NODRIVE = ME_FILE_DEL_NODRIVE;                  // 驱动器不存在
        public const uint EZNC_FILE_DEL_NOFILE = ME_FILE_DEL_NOFILE;                    // 文件不存在
        public const uint EZNC_FILE_DEL_NAMELENGTH = ME_FILE_DEL_NAMELENGTH;            // 名称长度错误
        public const uint EZNC_FILE_DEL_PROTECT = ME_FILE_DEL_PROTECT;                  // 文件受保护，无法删除
        public const uint EZNC_PCFILE_DEL_NOTDELETE = ME_PCFILE_DEL_NOTDELETE;          // PC文件未删除
        public const uint EZNC_PCFILE_DEL_ILLEGALNAME = ME_PCFILE_DEL_ILLEGALNAME;      // 非法名称错误
        public const uint EZNC_PCFILE_DEL_NODIR = ME_PCFILE_DEL_NODIR;                  // PC目录不存在
        public const uint EZNC_PCFILE_DEL_NODRIVE = ME_PCFILE_DEL_NODRIVE;              // PC驱动器不存在
        public const uint EZNC_PCFILE_DEL_NOFILE = ME_PCFILE_DEL_NOFILE;                // PC文件不存在

        // 文件重命名相关错误代码
        public const uint EZNC_FILE_REN_NOTRENAME = ME_FILE_REN_NOTRENAME;              // 无法重命名
        public const uint EZNC_FILE_REN_BUSY = ME_FILE_REN_BUSY;                        // 文件忙
        public const uint EZNC_FILE_REN_SAMENAME = ME_FILE_REN_SAMENAME;                // 目标名称与源名称相同
        public const uint EZNC_FILE_REN_FILEEXIST = ME_FILE_REN_FILEEXIST;              // 目标文件已存在
        public const uint EZNC_FILE_REN_FILESYSTEM = ME_FILE_REN_FILESYSTEM;            // 文件系统错误
        public const uint EZNC_FILE_REN_ILLEGALNAME = ME_FILE_REN_ILLEGALNAME;          // 非法名称错误
        public const uint EZNC_FILE_REN_NODIR = ME_FILE_REN_NODIR;                      // 目录不存在
        public const uint EZNC_FILE_REN_NODRIVE = ME_FILE_REN_NODRIVE;                  // 驱动器不存在
        public const uint EZNC_FILE_REN_NOFILE = ME_FILE_REN_NOFILE;                    // 源文件不存在
        public const uint EZNC_FILE_REN_NAMELENGTH = ME_FILE_REN_NAMELENGTH;            // 名称长度错误
        public const uint EZNC_FILE_REN_PROTECT = ME_FILE_REN_PROTECT;                  // 文件受保护，无法重命名
        public const uint EZNC_PCFILE_REN_NOTRENAME = ME_PCFILE_REN_NOTRENAME;          // PC文件无法重命名
        public const uint EZNC_PCFILE_REN_SAMENAME = ME_PCFILE_REN_SAMENAME;            // PC目标名称与源名称相同
        public const uint EZNC_PCFILE_REN_FILEEXIST = ME_PCFILE_REN_FILEEXIST;          // PC目标文件已存在
        public const uint EZNC_PCFILE_REN_ILLEGALNAME = ME_PCFILE_REN_ILLEGALNAME;      // PC非法名称错误
        public const uint EZNC_PCFILE_REN_NODIR = ME_PCFILE_REN_NODIR;                  // PC目录不存在
        public const uint EZNC_PCFILE_REN_NODRIVE = ME_PCFILE_REN_NODRIVE;              // PC驱动器不存在
        public const uint EZNC_PCFILE_REN_NOFILE = ME_PCFILE_REN_NOFILE;                // PC源文件不存在

        // 磁盘空间相关错误代码
        public const uint EZNC_FILE_DISKFREE_NODIR = ME_FILE_DISKFREE_NODIR;            // 目录不存在
        public const uint EZNC_FILE_DISKFREE_NODRIVE = ME_FILE_DISKFREE_NODRIVE;        // 驱动器不存在
        public const uint EZNC_FILE_DISKFREE_FILESYSTEM = ME_FILE_DISKFREE_FILESYSTEM;  // 文件系统错误
        public const uint EZNC_FILE_DISKFREE_NAMELENGTH = ME_FILE_DISKFREE_NAMELENGTH;  // 名称长度错误
        public const uint EZNC_FILE_DISKFREE_ILLEGALNAME = ME_FILE_DISKFREE_ILLEGALNAME; // 非法名称错误
        public const uint EZNC_PCFILE_DISKFREE_NODIR = ME_PCFILE_DISKFREE_NODIR;        // PC目录不存在
        public const uint EZNC_PCFILE_DISKFREE_NODRIVE = ME_PCFILE_DISKFREE_NODRIVE;    // PC驱动器不存在

        // 创建目录相关错误代码
        public const uint EZNC_FILE_CREATEDIR_FILEEXIST = ME_FILE_CREATEDIR_FILEEXIST;  // 目录已存在
        public const uint EZNC_FILE_CREATEDIR_FILESYSTEM = ME_FILE_CREATEDIR_FILESYSTEM;  // 文件系统错误
        public const uint EZNC_FILE_CREATEDIR_ILLEGALNAME = ME_FILE_CREATEDIR_ILLEGALNAME; // 非法名称错误
        public const uint EZNC_FILE_CREATEDIR_NODIR = ME_FILE_CREATEDIR_NODIR;          // 目录不存在
        public const uint EZNC_FILE_CREATEDIR_NOTSUPPORTED = ME_FILE_CREATEDIR_NOTSUPPORTED; // 不支持的操作
        public const uint EZNC_FILE_CREATEDIR_NAMELENGTH = ME_FILE_CREATEDIR_NAMELENGTH; // 名称长度错误
        public const uint EZNC_FILE_CREATEDIR_MEMORYOVER = ME_FILE_CREATEDIR_MEMORYOVER;  // 内存溢出
        public const uint EZNC_FILE_CREATEDIR_ALREADYOPENED = ME_FILE_CREATEDIR_ALREADYOPENED; // 目录已打开
        public const uint EZNC_FILE_CREATEDIR_ROOTDIRFULL = ME_FILE_CREATEDIR_ROOTDIRFULL; // 根目录已满
        public const uint EZNC_FILE_CREATEDIR_WRITEERR = ME_FILE_CREATEDIR_WRITEERR;      // 写入错误
        public const uint EZNC_FILE_CREATEDIR_WRITE_PROTECT = ME_FILE_CREATEDIR_WRITE_PROTECT; // 写保护错误
        public const uint EZNC_PCFILE_CREATEDIR_FILEEXIST = ME_PCFILE_CREATEDIR_FILEEXIST; // PC目录已存在
        public const uint EZNC_PCFILE_CREATEDIR_ILLEGALNAME = ME_PCFILE_CREATEDIR_ILLEGALNAME; // PC非法名称错误
        public const uint EZNC_PCFILE_CREATEDIR_NODIR = ME_PCFILE_CREATEDIR_NODIR;      // PC目录不存在
        public const uint EZNC_PCFILE_CREATEDIR_NAMELENGTH = ME_PCFILE_CREATEDIR_NAMELENGTH; // PC名称长度错误
        public const uint EZNC_PCFILE_CREATEDIR_MEMORYOVER = ME_PCFILE_CREATEDIR_MEMORYOVER; // PC内存溢出
        public const uint EZNC_PCFILE_CREATEDIR_ALREADYOPENED = ME_PCFILE_CREATEDIR_ALREADYOPENED; // PC目录已打开
        public const uint EZNC_PCFILE_CREATEDIR_ROOTDIRFULL = ME_PCFILE_CREATEDIR_ROOTDIRFULL; // PC根目录已满
        public const uint EZNC_PCFILE_CREATEDIR_WRITEERR = ME_PCFILE_CREATEDIR_WRITEERR; // PC写入错误


        // 删除目录相关错误代码
        public const uint EZNC_FILE_DELETEDIR_FILESYSTEM = ME_FILE_DELETEDIR_FILESYSTEM;  // 文件系统错误
        public const uint EZNC_FILE_DELETEDIR_ILLEGALNAME = ME_FILE_DELETEDIR_ILLEGALNAME; // 非法名称错误
        public const uint EZNC_FILE_DELETEDIR_NODIR = ME_FILE_DELETEDIR_NODIR;            // 目录不存在
        public const uint EZNC_FILE_DELETEDIR_NOTSUPPORTED = ME_FILE_DELETEDIR_NOTSUPPORTED; // 不支持的操作
        public const uint EZNC_FILE_DELETEDIR_NAMELENGTH = ME_FILE_DELETEDIR_NAMELENGTH; // 名称长度错误
        public const uint EZNC_FILE_DELETEDIR_NOTEMPTY = ME_FILE_DELETEDIR_NOTEMPTY;      // 目录不为空
        public const uint EZNC_FILE_DELETEDIR_ALREADYOPENED = ME_FILE_DELETEDIR_ALREADYOPENED; // 目录已打开
        public const uint EZNC_FILE_DELETEDIR_NOTDELETE = ME_FILE_DELETEDIR_NOTDELETE;    // 目录未删除
        public const uint EZNC_FILE_DELETEDIR_WRITE_PROTECT = ME_FILE_DELETEDIR_WRITE_PROTECT; // 写保护错误
        public const uint EZNC_PCFILE_DELETEDIR_ILLEGALNAME = ME_PCFILE_DELETEDIR_ILLEGALNAME; // PC非法名称错误
        public const uint EZNC_PCFILE_DELETEDIR_NODIR = ME_PCFILE_DELETEDIR_NODIR;        // PC目录不存在
        public const uint EZNC_PCFILE_DELETEDIR_NAMELENGTH = ME_PCFILE_DELETEDIR_NAMELENGTH; // PC名称长度错误
        public const uint EZNC_PCFILE_DELETEDIR_NOTEMPTY = ME_PCFILE_DELETEDIR_NOTEMPTY;  // PC目录不为空
        public const uint EZNC_PCFILE_DELETEDIR_ALREADYOPENED = ME_PCFILE_DELETEDIR_ALREADYOPENED; // PC目录已打开
        public const uint EZNC_PCFILE_DELETEDIR_NOTDELETE = ME_PCFILE_DELETEDIR_NOTDELETE; // PC目录未删除



        // 网络相关错误代码
        public const uint EZNC_ENET_ALREADYOPEN = ME_ENET_ALREADYOPEN;                  // 网络已打开
        public const uint EZNC_ENET_NOTOPEN = ME_ENET_NOTOPEN;                          // 网络未打开
        public const uint EZNC_ENET_CARDNOTEXIST = ME_ENET_CARDNOTEXIST;                // 网络卡不存在
        public const uint EZNC_ENET_BADCHANNEL = ME_ENET_BADCHANNEL;                    // 通道错误
        public const uint EZNC_ENET_BADFD = ME_ENET_BADFD;                              // 文件描述符错误
        public const uint EZNC_ENET_NOTCONNECT = ME_ENET_NOTCONNECT;                    // 未连接
        public const uint EZNC_ENET_NOTCLOSE = ME_ENET_NOTCLOSE;                        // 无法关闭
        public const uint EZNC_ENET_TIMEOUT = ME_ENET_TIMEOUT;                          // 超时
        public const uint EZNC_ENET_DATAERR = ME_ENET_DATAERR;                          // 数据错误
        public const uint EZNC_ENET_CANCELED = ME_ENET_CANCELED;                        // 操作已取消
        public const uint EZNC_ENET_ILLEGALSIZE = ME_ENET_ILLEGALSIZE;                  // 非法大小
        public const uint EZNC_ENET_TASKQUIT = ME_ENET_TASKQUIT;                        // 任务退出
        public const uint EZNC_ENET_UNKNOWNFUNC = ME_ENET_UNKNOWNFUNC;                  // 未知函数
        public const uint EZNC_ENET_SETDATAERR = ME_ENET_SETDATAERR;                    // 设置数据错误

        // 缓存相关错误代码
        public const uint EZNC_READ_CACHE_ADDR = ME_READ_CACHE_ADDR;                    // 缓存地址错误
        public const uint EZNC_READ_CACHE_DATA = ME_READ_CACHE_DATA;                    // 缓存数据错误
        public const uint EZNC_READ_CACHE_SECT = ME_READ_CACHE_SECT;                    // 缓存扇区错误
        public const uint EZNC_READ_CACHE_SUBSECT = ME_READ_CACHE_SUBSECT;              // 缓存子扇区错误
        public const uint EZNC_READ_CACHE_AXIS = ME_READ_CACHE_AXIS;                    // 缓存轴错误
        public const uint EZNC_READ_CACHE_WRITEONLY = ME_READ_CACHE_WRITEONLY;          // 只写缓存错误
        public const uint EZNC_READ_CACHE_READ = ME_READ_CACHE_READ;                    // 读取缓存错误
        public const uint EZNC_READ_CACHE_DATATYPE = ME_READ_CACHE_DATATYPE;            // 数据类型错误
        public const uint EZNC_READ_CACHE_REGIST = ME_READ_CACHE_REGIST;                // 注册缓存错误

        // 文件系统打开相关错误代码
        public const uint EZNC_FS_OPEN_FILE_FILEFULL = ME_FS_OPEN_FILE_FILEFULL;        // 文件已满
        public const uint EZNC_FS_OPEN_FILE_ALREADYOPEN = ME_FS_OPEN_FILE_ALREADYOPEN;  // 文件已打开
        public const uint EZNC_FS_OPEN_FILE_BUSY = ME_FS_OPEN_FILE_BUSY;                // 文件忙
        public const uint EZNC_FS_OPEN_FILE_OPEN = ME_FS_OPEN_FILE_OPEN;                // 文件打开
        public const uint EZNC_FS_OPEN_FILE_MALLOC = ME_FS_OPEN_FILE_MALLOC;            // 内存分配失败
        public const uint EZNC_FS_OPEN_FILE_NOTSUPPORTED = ME_FS_OPEN_FILE_NOTSUPPORTED; // 不支持的操作
        public const uint EZNC_FS_OPEN_FILE_NODRIVE = ME_FS_OPEN_FILE_NODRIVE;          // 驱动器不存在
        public const uint EZNC_FS_OPEN_FILE_NAMELENGTH = ME_FS_OPEN_FILE_NAMELENGTH;    // 名称长度错误

        public const uint EZNC_FS_OPEN_FILE_SORT = ME_FS_OPEN_FILE_SORT;                // 排序文件错误
        public const uint EZNC_FS_OPEN_FILE_SAFE_NOPASSWD = ME_FS_OPEN_FILE_SAFE_NOPASSWD; // 无密码安全文件
        public const uint EZNC_FS_OPEN_FILE_PROTECT = ME_FS_OPEN_FILE_PROTECT;          // 文件保护错误
        public const uint EZNC_FS_OPEN_FILE_WRITE_PROTECT = ME_FS_OPEN_FILE_WRITE_PROTECT; // 写保护错误
        public const uint EZNC_FS_OPEN_FILE_ENTRYOVER = ME_FS_OPEN_FILE_ENTRYOVER;      // 条目溢出
        public const uint EZNC_FS_OPEN_FILE_ILLEGALNAME = ME_FS_OPEN_FILE_ILLEGALNAME;  // 非法名称错误

        // 文件系统关闭相关错误代码
        public const uint EZNC_FS_CLOSE_FILE_NOTOPEN = ME_FS_CLOSE_FILE_NOTOPEN;        // 文件未打开

        // 文件系统创建相关错误代码
        public const uint EZNC_FS_CREATE_FILE_FILEFULL = ME_FS_CREATE_FILE_FILEFULL;    // 文件已满
        public const uint EZNC_FS_CREATE_FILE_ALREADYOPEN = ME_FS_CREATE_FILE_ALREADYOPEN; // 文件已打开
        public const uint EZNC_FS_CREATE_FILE_BUSY = ME_FS_CREATE_FILE_BUSY;            // 文件忙
        public const uint EZNC_FS_CREATE_FILE_CREATE = ME_FS_CREATE_FILE_CREATE;        // 文件创建
        public const uint EZNC_FS_CREATE_FILE_MALLOC = ME_FS_CREATE_FILE_MALLOC;        // 内存分配失败
        public const uint EZNC_FS_CREATE_FILE_NOTSUPPORTED = ME_FS_CREATE_FILE_NOTSUPPORTED; // 不支持的操作
        public const uint EZNC_FS_CREATE_FILE_NODRIVE = ME_FS_CREATE_FILE_NODRIVE;      // 驱动器不存在
        public const uint EZNC_FS_CREATE_FILE_NAMELENGTH = ME_FS_CREATE_FILE_NAMELENGTH; // 名称长度错误

        // 文件系统读取相关错误代码
        public const uint EZNC_FS_READ_FILE_NOTOPEN = ME_FS_READ_FILE_NOTOPEN;          // 文件未打开
        public const uint EZNC_FS_READ_FILE_READ = ME_FS_READ_FILE_READ;                // 读取错误

        // 文件系统写入相关错误代码
        public const uint EZNC_FS_WRITE_FILE_NOTOPEN = ME_FS_WRITE_FILE_NOTOPEN;        // 文件未打开
        public const uint EZNC_FS_WRITE_FILE_WRITE = ME_FS_WRITE_FILE_WRITE;            // 写入错误
        public const uint EZNC_FS_WRITE_FILE_NOTSUPPORTED = ME_FS_WRITE_FILE_NOTSUPPORTED; // 不支持的操作

        // 文件系统删除相关错误代码
        public const uint EZNC_FS_REMOVE_FILE_ALREADYOPEN = ME_FS_REMOVE_FILE_ALREADYOPEN; // 文件已打开
        public const uint EZNC_FS_REMOVE_FILE_BUSY = ME_FS_REMOVE_FILE_BUSY;            // 文件忙
        public const uint EZNC_FS_REMOVE_FILE_NOFILE = ME_FS_REMOVE_FILE_NOFILE;        // 文件不存在
        public const uint EZNC_FS_REMOVE_FILE_REMOVEERR = ME_FS_REMOVE_FILE_REMOVEERR;  // 删除错误
        public const uint EZNC_FS_REMOVE_FILE_NOTSUPPORTED = ME_FS_REMOVE_FILE_NOTSUPPORTED; // 不支持的操作
        public const uint EZNC_FS_REMOVE_FILE_NODRIVE = ME_FS_REMOVE_FILE_NODRIVE;      // 驱动器不存在
        public const uint EZNC_FS_REMOVE_FILE_NAMELENGTH = ME_FS_REMOVE_FILE_NAMELENGTH; // 名称长度错误
        public const uint EZNC_FS_REMOVE_FILE_PROTECT = ME_FS_REMOVE_FILE_PROTECT;      // 文件保护错误
        public const uint EZNC_FS_REMOVE_FILE_WRITE_PROTECT = ME_FS_REMOVE_FILE_WRITE_PROTECT; // 写保护错误


        // 文件重命名相关错误代码
        public const uint EZNC_FS_RENAME_FILE_NOFILE = ME_FS_RENAME_FILE_NOFILE;              // 文件不存在
        public const uint EZNC_FS_RENAME_FILE_ALREADYOPEN = ME_FS_RENAME_FILE_ALREADYOPEN;    // 文件已打开
        public const uint EZNC_FS_RENAME_FILE_FILEFULL = ME_FS_RENAME_FILE_FILEFULL;          // 文件已满
        public const uint EZNC_FS_RENAME_FILE_NOTRENAME = ME_FS_RENAME_FILE_NOTRENAME;        // 无法重命名
        public const uint EZNC_FS_RENAME_FILE_NOTSUPPORTED = ME_FS_RENAME_FILE_NOTSUPPORTED;  // 不支持的操作
        public const uint EZNC_FS_RENAME_FILE_NODRIVE = ME_FS_RENAME_FILE_NODRIVE;            // 驱动器不存在
        public const uint EZNC_FS_RENAME_FILE_NAMELENGTH = ME_FS_RENAME_FILE_NAMELENGTH;      // 名称长度错误

        // IO控制相关错误代码
        public const uint EZNC_FS_IOCTL_FILE_NOTOPEN = ME_FS_IOCTL_FILE_NOTOPEN;              // 文件未打开
        public const uint EZNC_FS_IOCTL_FILE_READ = ME_FS_IOCTL_FILE_READ;                    // 读取错误
        public const uint EZNC_FS_IOCTL_FILE_WRITE = ME_FS_IOCTL_FILE_WRITE;                  // 写入错误
        public const uint EZNC_FS_IOCTL_FILE_FUNCTION = ME_FS_IOCTL_FILE_FUNCTION;              // 功能错误
        public const uint EZNC_FS_IOCTL_FILE_NOTSUPPORTED = ME_FS_IOCTL_FILE_NOTSUPPORTED;    // 不支持的操作    
        public const uint EZNC_FS_IOCTL_FILE_DATATYPE = ME_FS_IOCTL_FILE_DATATYPE;            // 数据类型错误
        public const uint EZNC_FS_IOCTL_FILE_DATASIZE = ME_FS_IOCTL_FILE_DATASIZE;            // 数据大小错误

        // 目录打开相关错误代码
        public const uint EZNC_FS_OPEN_DIR_FILEFULL = ME_FS_OPEN_DIR_FILEFULL;                // 目录已满
        public const uint EZNC_FS_OPEN_DIR_NOTOPEN = ME_FS_OPEN_DIR_NOTOPEN;                  // 目录未打开
        public const uint EZNC_FS_OPEN_DIR_BUSY = ME_FS_OPEN_DIR_BUSY;                        // 目录忙
        public const uint EZNC_FS_OPEN_DIR_NODIR = ME_FS_OPEN_DIR_NODIR;                      // 目录不存在
        public const uint EZNC_FS_OPEN_DIR_MALLOC = ME_FS_OPEN_DIR_MALLOC;                    // 内存分配失败
        public const uint EZNC_FS_OPEN_DIR_NOTSUPPORTED = ME_FS_OPEN_DIR_NOTSUPPORTED;        // 不支持的操作
        public const uint EZNC_FS_OPEN_DIR_NODRIVE = ME_FS_OPEN_DIR_NODRIVE;                  // 驱动器不存在
        public const uint EZNC_FS_OPEN_DIR_NAMELENGTH = ME_FS_OPEN_DIR_NAMELENGTH;            // 名称长度错误

        // 目录读取相关错误代码
        public const uint EZNC_FS_READ_DIR_NOTOPEN = ME_FS_READ_DIR_NOTOPEN;                  // 目录未打开
        public const uint EZNC_FS_READ_DIR_NODIR = ME_FS_READ_DIR_NODIR;                      // 目录不存在
        public const uint EZNC_FS_READ_DIR_DATASIZE = ME_FS_READ_DIR_DATASIZE;                // 数据大小错误

        // 目录关闭相关错误代码
        public const uint EZNC_FS_CLOSE_DIR_NOTOPEN = ME_FS_CLOSE_DIR_NOTOPEN;                // 目录未打开

        // 文件状态相关错误代码
        public const uint EZNC_FS_STAT_FILE_FILEFULL = ME_FS_STAT_FILE_FILEFULL;              // 文件已满
        public const uint EZNC_FS_STAT_FILE_STATERR = ME_FS_STAT_FILE_STATERR;                // 状态错误
        public const uint EZNC_FS_STAT_FILE_NOTSUPPORTED = ME_FS_STAT_FILE_NOTSUPPORTED;      // 不支持的操作
        public const uint EZNC_FS_STAT_FILE_NODRIVE = ME_FS_STAT_FILE_NODRIVE;                // 驱动器不存在
        public const uint EZNC_FS_STAT_FILE_NAMELENGTH = ME_FS_STAT_FILE_NAMELENGTH;          // 名称长度错误

        // 文件状态相关错误代码（使用文件句柄）
        public const uint EZNC_FS_FSTAT_FILE_NOTOPEN = ME_FS_FSTAT_FILE_NOTOPEN;              // 文件未打开
        public const uint EZNC_FS_FSTAT_FILE_STATERR = ME_FS_FSTAT_FILE_STATERR;              // 状态错误
        public const uint EZNC_FS_FSTAT_FILE_NOTSUPPORTED = ME_FS_FSTAT_FILE_NOTSUPPORTED;    // 不支持的操作
        public const uint EZNC_FS_FSTAT_FILE_NODRIVE = ME_FS_FSTAT_FILE_NODRIVE;              // 驱动器不存在
        public const uint EZNC_FS_FSTAT_FILE_NAMELENGTH = ME_FS_FSTAT_FILE_NAMELENGTH;        // 名称长度错误

        // IO控制用户打开格式相关错误代码
        public const uint EZNC_FS_IOCTL_UOPEN_FORMAT = ME_FS_IOCTL_UOPEN_FORMAT;              // 用户打开格式错误


    }
}
