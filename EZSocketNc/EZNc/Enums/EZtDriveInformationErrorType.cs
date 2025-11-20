using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZSocketNc.EZNc
{
    /// <summary>
    /// 链接错误类型
    /// 网络相关错误代码
    /// </summary>
    public enum EZtDriveInformationErrorType : uint
    {
        /// <summary>
        /// 正常结束
        /// </summary>
        [Description("正常结束")]
        OK = 0,// 正常结束
        /// <summary>
        /// 驱动信息读取错误
        /// </summary>
        [Description("驱动信息读取错误")]
        ZNC_FILE_DRVLIST_READ = 0x80030794,

        ///// <summary>
        ///// 驱动器不存在
        ///// </summary>
        //[Description("驱动器不存在")]
        //EZNC_FILE_DIR_NODRIVE =0x8003019B


        // 文件打开相关错误代码
        /// <summary>
        /// 文件打开模式错误
        /// </summary>
        [Description("文件打开模式错误")]
        EZNC_FILE_OPEN_MODE = 0x80B00201, // 文件打开模式错误
        /// <summary>
        /// 文件未打开
        /// </summary>
        [Description("文件未打开")]
        EZNC_FILE_OPEN_NOTOPEN = 0x80B00202, // 文件未打开
        /// <summary>
        /// 文件已存在
        /// </summary>
        [Description("文件已存在")]
        EZNC_FILE_OPEN_FILEEXIST = 0x80B00203, // 文件已存在
        /// <summary>
        /// 文件已经打开
        /// </summary>
        [Description("正常结束")]
        EZNC_FILE_OPEN_ALREADYOPENED = 0x80B00204, // 文件已经打开
        /// <summary>
        /// 创建文件错误
        /// </summary>
        [Description("创建文件错误")]
        EZNC_FILE_OPEN_CREATE = 0x80B00205, // 创建文件错误
        /// <summary>
        /// 写入文件时未打开
        /// </summary>
        [Description("写入文件时未打开")]
        EZNC_FILE_WRITEFILE_NOTOPEN = 0x80B00206, // 写入文件时未打开
        /// <summary>
        /// 写入文件长度错误
        /// </summary>
        [Description("写入文件长度错误")]
        EZNC_FILE_WRITEFILE_LENGTH = 0x80B00207, // 写入文件长度错误
        /// <summary>
        /// 写入文件错误
        /// </summary>
        [Description("写入文件错误")]
        EZNC_FILE_WRITEFILE_WRITE = 0x80B00208, // 写入文件错误
        /// <summary>
        /// 读取文件时未打开
        /// </summary>
        [Description("读取文件时未打开")]
        EZNC_FILE_READFILE_NOTOPEN = 0x80B00209, // 读取文件时未打开
        /// <summary>
        /// 读取文件错误
        /// </summary>
        [Description("读取文件错误")]
        EZNC_FILE_READFILE_READ = 0x80B0020A, // 读取文件错误
        /// <summary>
        /// 创建文件错误
        /// </summary>
        [Description("创建文件错误")]
        EZNC_FILE_READFILE_CREATE = 0x80B0020B, // 创建文件错误
        /// <summary>
        /// 文件不存在
        /// </summary>
        [Description("文件不存在")]
        EZNC_FILE_OPEN_FILENOTEXIST = 0x80B0020C, // 文件不存在
        /// <summary>
        /// 打开文件错误
        /// </summary>
        [Description("打开文件错误")]
        EZNC_FILE_OPEN_OPEN = 0x80B0020D, // 打开文件错误
        /// <summary>
        /// 文件路径非法
        /// </summary>
        [Description("文件路径非法")]
        EZNC_FILE_OPEN_ILLEGALPATH = 0x80B0020E, // 文件路径非法
        /// <summary>
        /// 读取的文件非法
        /// </summary>
        [Description("读取的文件非法")]
        EZNC_FILE_READFILE_ILLEGALFILE = 0x80B0020F, // 读取的文件非法
        /// <summary>
        /// 写入的文件非法
        /// </summary>
        [Description("写入的文件非法")]
        EZNC_FILE_WRITEFILE_ILLEGALFILE = 0x80B00210, // 写入的文件非法


        // 文件目录相关错误代码
        /// <summary>
        /// 目录已打开
        /// </summary>
        [Description("目录已打开")]
        EZNC_FILE_DIR_ALREADYOPENED = 0x80030101,            // 目录已打开
        /// <summary>
        /// 数据大小错误
        /// </summary>
        [Description("数据大小错误")]
        EZNC_FILE_DIR_DATASIZE = 0x80030103,                      // 数据大小错误
        /// <summary>
        /// 目录未打开
        /// </summary>
        [Description("目录未打开")]
        EZNC_FILE_DIR_NOTOPEN = 0x80030190,                        // 目录未打开
        /// <summary>
        /// 读取错误
        /// </summary>
        [Description("读取错误")]
        EZNC_FILE_DIR_READ = 0x80030194,                              // 读取错误
        /// <summary>
        /// 文件系统错误
        /// </summary>
        [Description("文件系统错误")]
        EZNC_FILE_DIR_FILESYSTEM = 0x80030143,                  // 文件系统错误
        /// <summary>
        /// 目录不存在错误
        /// </summary>
        [Description("目录不存在错误")]
        EZNC_FILE_DIR_NODIR = 0x80030191,                            // 目录不存在错误
        /// <summary>
        /// 驱动器不存在错误
        /// </summary>
        [Description("驱动器不存在错误")]
        EZNC_FILE_DIR_NODRIVE = 0x8003019B,                        // 驱动器不存在错误
        /// <summary>
        /// 名称长度错误
        /// </summary>
        [Description("名称长度错误")]
        EZNC_FILE_DIR_NAMELENGTH = 0x80030148,                  // 名称长度错误
        /// <summary>
        /// 非法名称错误
        /// </summary>
        [Description("非法名称错误")]
        EZNC_FILE_DIR_ILLEGALNAME = 0x80030198,                // 非法名称错误
        /// <summary>
        /// 目录不存在错误
        /// </summary>
        [Description("目录不存在错误")]
        EZNC_PCFILE_DIR_NODIR = 0x800301A2,                        // 目录不存在错误
        /// <summary>
        /// 文件不存在错误
        /// </summary>
        [Description("文件不存在错误")]
        EZNC_PCFILE_DIR_NOFILE = 0x80030142,                      // 文件不存在错误
        /// <summary>
        /// 驱动器不存在错误
        /// </summary>
        [Description("驱动器不存在错误")]
        EZNC_PCFILE_DIR_NODRIVE = 0x800301A8,                    // 驱动器不存在错误
        /// <summary>
        /// 目录未打开错误
        /// </summary>
        [Description("目录未打开错误")]
        EZNC_PCFILE_DIR_NOTOPEN = 0x800301A0,                    // 目录未打开错误
        /// <summary>
        /// 读取错误
        /// </summary>
        [Description("读取错误")]
        EZNC_PCFILE_DIR_READ = 0x800301A5,                          // 读取错误
        /// <summary>
        /// PC目录已打开
        /// </summary>
        [Description("PC目录已打开")]
        EZNC_PCFILE_DIR_ALREADYOPENED = 0x80030102,        // PC目录已打开

        //// 文件复制相关错误代码
        /// <summary>
        /// 文件忙
        /// </summary>
        [Description("文件忙")]
        EZNC_FILE_COPY_BUSY = 0x80030447,                            // 文件忙
        /// <summary>
        /// 条目溢出错误
        /// </summary>
        [Description("条目溢出错误")]
        EZNC_FILE_COPY_ENTRYOVER = 0x80030403,                  // 条目溢出错误
        /// <summary>
        /// 文件已存在
        /// </summary>
        [Description("文件已存在")]
        EZNC_FILE_COPY_FILEEXIST = 0x80030401,                  // 文件已存在
        /// <summary>
        /// 文件系统错误
        /// </summary>
        [Description("文件系统错误")]
        EZNC_FILE_COPY_FILESYSTEM = 0x80030443,                // 文件系统错误
        /// <summary>
        /// 非法名称错误
        /// </summary>
        [Description("非法名称错误")]
        EZNC_FILE_COPY_ILLEGALNAME = 0x80030498,              // 非法名称错误
        /// <summary>
        /// 内存溢出错误
        /// </summary>
        [Description("内存溢出错误")]
        EZNC_FILE_COPY_MEMORYOVER = 0x80030404,                // 内存溢出错误
        /// <summary>
        /// 名称长度错误
        /// </summary>
        [Description("名称长度错误")]
        EZNC_FILE_COPY_NAMELENGTH = 0x80030448,                // 名称长度错误
        /// <summary>
        /// 保护错误
        /// </summary>
        [Description("保护错误")]
        EZNC_FILE_COPY_PROTECT = 0x8003044A,                      // 保护错误
        /// <summary>
        /// 目录不存在错误
        /// </summary>
        [Description("目录不存在错误")]
        EZNC_FILE_COPY_NODIR = 0x80030491,                          // 目录不存在错误
        /// <summary>
        /// 驱动器不存在错误
        /// </summary>
        [Description("驱动器不存在错误")]
        EZNC_FILE_COPY_NODRIVE = 0x8003049B,                      // 驱动器不存在错误
        /// <summary>
        /// 文件不存在错误
        /// </summary>
        [Description("文件不存在错误")]
        EZNC_FILE_COPY_NOFILE = 0x80030442,                        // 文件不存在错误
        /// <summary>
        /// PLC运行错误
        /// </summary>
        [Description("PLC运行错误")]
        EZNC_FILE_COPY_PLCRUN = 0x80030446,                        // PLC运行错误
        /// <summary>
        /// 读取错误
        /// </summary>
        [Description("读取错误")]
        EZNC_FILE_COPY_READ = 0x80030494,                            // 读取错误
        /// <summary>
        /// 写入错误
        /// </summary>
        [Description("写入错误")]
        EZNC_FILE_COPY_WRITE = 0x80030495,                          // 写入错误
        /// <summary>
        /// 写入警告
        /// </summary>
        [Description("写入警告")]
        EZNC_FILE_COPY_WRITE_WARNING = 0x80030495,          // 写入警告
        /// <summary>
        /// 不同错误,差异
        /// </summary>
        [Description("不同错误")]
        EZNC_FILE_COPY_DIFFER = 0x80030405,                        // 不同错误
        /// <summary>
        /// 不支持错误
        /// </summary>
        [Description("不支持错误")]
        EZNC_FILE_COPY_NOTSUPPORTED = 0x80030449,            // 不支持错误
        /// <summary>
        /// 文件未打开
        /// </summary>
        [Description("文件未打开")]
        EZNC_FILE_COPY_NOTOPEN = 0x80030490,                      // 文件未打开
        /// <summary>
        /// 执行中错误
        /// </summary>
        [Description("执行中错误")]
        EZNC_FILE_COPY_EXECUTING = 0x8003044C,                  // 执行中错误
        /// <summary>
        /// 安全密码锁定错误
        /// </summary>
        [Description("安全密码锁定错误")]
        EZNC_FILE_COPY_SAFETYPWLOCK = 0x8003044D,            // 安全密码锁定错误
        /// <summary>
        /// 非法格式错误,文件格式非法
        /// </summary>
        [Description("非法格式错误")]
        EZNC_FILE_COPY_ILLEGALFORMAT = 0x8003049D,          // 非法格式错误
        /// <summary>
        /// 密码错误
        /// </summary>
        [Description("密码错误")]
        EZNC_FILE_COPY_WRONGPASSWORD = 0x8003049E,          // 密码错误
        /// <summary>
        /// 创建错误
        /// </summary>
        [Description("创建错误")]
        EZNC_PCFILE_COPY_CREATE = 0x800304A4,                    // 创建错误
        /// <summary>
        /// 复制打开错误
        /// </summary>
        [Description("打开错误")]
        EZNC_PCFILE_COPY_OPEN = 0x800304A3,                        // 打开错误
        /// <summary>
        /// 文件已存在
        /// </summary>
        [Description("文件已存在")]
        EZNC_PCFILE_COPY_FILEEXIST = 0x80030402,              // 文件已存在
        /// <summary>
        /// 非法名称错误
        /// </summary>
        [Description("非法名称错误")]
        EZNC_PCFILE_COPY_ILLEGALNAME = 0x800304A7,          // 非法名称错误
        /// <summary>
        /// 目录不存在错误
        /// </summary>
        [Description("目录不存在错误")]
        EZNC_PCFILE_COPY_NODIR = 0x800304A2,                      // 目录不存在错误
        /// <summary>
        /// 驱动器不存在错误
        /// </summary>
        [Description("PC目录已打开")]
        EZNC_PCFILE_COPY_NODRIVE = 0x800304A8,                  // 驱动器不存在错误
        /// <summary>
        /// 文件不存在错误
        /// </summary>
        [Description("文件不存在错误")]
        EZNC_PCFILE_COPY_NOFILE = 0x800304A1,                    // 文件不存在错误
        /// <summary>
        /// 读取错误
        /// </summary>
        [Description("读取错误")]
        EZNC_PCFILE_COPY_READ = 0x800304A5,                        // 读取错误
        /// <summary>
        /// 写入错误
        /// </summary>
        [Description("写入错误")]
        EZNC_PCFILE_COPY_WRITE = 0x800304A6,                      // 写入错误
        /// <summary>
        /// 文件未打开
        /// </summary>
        [Description("文件未打开")]
        EZNC_PCFILE_COPY_NOTOPEN = 0x800304A0,                  // 文件未打开
        /// <summary>
        /// 内存溢出错误
        /// </summary>
        [Description("内存溢出错误")]
        EZNC_PCFILE_COPY_MEMORYOVER = 0x80030406,            // 内存溢出错误

        //// 文件删除相关错误代码
        /// <summary>
        /// 文件忙
        /// </summary>
        [Description("文件忙")]
        EZNC_FILE_DEL_BUSY = 0x80030247,                        // 文件忙
        EZNC_FILE_DEL_NOTDELETE = 0x80030201,              // 文件未删除
        EZNC_FILE_DEL_FILESYSTEM = 0x80030243,            // 文件系统错误
        EZNC_FILE_DEL_ILLEGALNAME = 0x80030298,          // 非法名称错误
        EZNC_FILE_DEL_NODIR = 0x80030291,                      // 目录不存在
        EZNC_FILE_DEL_NODRIVE = 0x8003029B,                  // 驱动器不存在
        EZNC_FILE_DEL_NOFILE = 0x80030242,                    // 文件不存在
        EZNC_FILE_DEL_NAMELENGTH = 0x80030248,            // 名称长度错误
        EZNC_FILE_DEL_PROTECT = 0x8003024A,                  // 文件受保护，无法删除
        EZNC_PCFILE_DEL_NOTDELETE = 0x80030202,          // PC文件未删除
        EZNC_PCFILE_DEL_ILLEGALNAME = 0x800302A7,      // 非法名称错误
        EZNC_PCFILE_DEL_NODIR = 0x800302A2,                  // PC目录不存在
        EZNC_PCFILE_DEL_NODRIVE = 0x800302A8,              // PC驱动器不存在
        EZNC_PCFILE_DEL_NOFILE = 0x800302A1,                // PC文件不存在

        //// 文件重命名相关错误代码
        EZNC_FILE_REN_NOTRENAME = 0x80030303,              // 无法重命名
        EZNC_FILE_REN_BUSY = 0x80030347,                        // 文件忙
        EZNC_FILE_REN_SAMENAME = 0x80030305,                // 目标名称与源名称相同
        EZNC_FILE_REN_FILEEXIST = 0x80030301,              // 目标文件已存在
        EZNC_FILE_REN_FILESYSTEM = 0x80030343,            // 文件系统错误
        EZNC_FILE_REN_ILLEGALNAME = 0x80030398,          // 非法名称错误
        EZNC_FILE_REN_NODIR = 0x80030391,                      // 目录不存在
        EZNC_FILE_REN_NODRIVE = 0x8003039B,                  // 驱动器不存在
        EZNC_FILE_REN_NOFILE = 0x80030342,                    // 源文件不存在
        EZNC_FILE_REN_NAMELENGTH = 0x80030348,            // 名称长度错误
        EZNC_FILE_REN_PROTECT = 0x8003034A,                  // 文件受保护，无法重命名
        EZNC_PCFILE_REN_NOTRENAME = 0x80030304,          // PC文件无法重命名
        EZNC_PCFILE_REN_SAMENAME = 0x80030306,            // PC目标名称与源名称相同
        EZNC_PCFILE_REN_FILEEXIST = 0x80030302,          // PC目标文件已存在
        EZNC_PCFILE_REN_ILLEGALNAME = 0x800303A7,      // PC非法名称错误
        EZNC_PCFILE_REN_NODIR = 0x800303A2,                  // PC目录不存在
        EZNC_PCFILE_REN_NODRIVE = 0x800303A8,              // PC驱动器不存在
        EZNC_PCFILE_REN_NOFILE = 0x800303A1,                // PC源文件不存在

        //// 磁盘空间相关错误代码
        EZNC_FILE_DISKFREE_NODIR = 0x80030691,            // 目录不存在
        EZNC_FILE_DISKFREE_NODRIVE = 0x8003069B,        // 驱动器不存在
        EZNC_FILE_DISKFREE_FILESYSTEM = 0x80030643,  // 文件系统错误
        EZNC_FILE_DISKFREE_NAMELENGTH = 0x80030648,  // 名称长度错误
        EZNC_FILE_DISKFREE_ILLEGALNAME = 0x80030648, // 非法名称错误
        EZNC_PCFILE_DISKFREE_NODIR = 0x800306A2,        // PC目录不存在
        EZNC_PCFILE_DISKFREE_NODRIVE = 0x800306A8,    // PC驱动器不存在

        //// 创建目录相关错误代码
        EZNC_FILE_CREATEDIR_FILEEXIST = 0x80030A03,  // 目录已存在
        EZNC_FILE_CREATEDIR_FILESYSTEM = 0x80030A43,  // 文件系统错误
        EZNC_FILE_CREATEDIR_ILLEGALNAME = 0x80030A98, // 非法名称错误
        EZNC_FILE_CREATEDIR_NODIR = 0x80030A91,          // 目录不存在
        EZNC_FILE_CREATEDIR_NOTSUPPORTED = 0x80030A49, // 不支持的操作
        EZNC_FILE_CREATEDIR_NAMELENGTH = 0x80030A48, // 名称长度错误
        EZNC_FILE_CREATEDIR_MEMORYOVER = 0x80030A05,  // 内存溢出
        EZNC_FILE_CREATEDIR_ALREADYOPENED = 0x80030A01, // 目录已打开
        EZNC_FILE_CREATEDIR_ROOTDIRFULL = 0x80030A07, // 根目录已满
        EZNC_FILE_CREATEDIR_WRITEERR = 0x80030A95,      // 写入错误
        EZNC_FILE_CREATEDIR_WRITE_PROTECT = 0x80030AB2, // 写保护错误
        EZNC_PCFILE_CREATEDIR_FILEEXIST = 0x80030A04, // PC目录已存在
        EZNC_PCFILE_CREATEDIR_ILLEGALNAME = 0x80030AA7, // PC非法名称错误
        EZNC_PCFILE_CREATEDIR_NODIR = 0x80030AA2,      // PC目录不存在
        EZNC_PCFILE_CREATEDIR_NAMELENGTH = 0x80030AA9, // PC名称长度错误
        EZNC_PCFILE_CREATEDIR_MEMORYOVER = 0x80030A06, // PC内存溢出
        EZNC_PCFILE_CREATEDIR_ALREADYOPENED = 0x80030A02, // PC目录已打开
        EZNC_PCFILE_CREATEDIR_ROOTDIRFULL = 0x80030A08, // PC根目录已满
        EZNC_PCFILE_CREATEDIR_WRITEERR = 0x80030AA6, // PC写入错误


        // 删除目录相关错误代码
        EZNC_FILE_DELETEDIR_FILESYSTEM = 0x80030B43,  // 文件系统错误
        EZNC_FILE_DELETEDIR_ILLEGALNAME = 0x80030B98, // 非法名称错误
        EZNC_FILE_DELETEDIR_NODIR = 0x80030B91,            // 目录不存在
        EZNC_FILE_DELETEDIR_NOTSUPPORTED = 0x80030B49, // 不支持的操作
        EZNC_FILE_DELETEDIR_NAMELENGTH = 0x80030B48, // 名称长度错误
        EZNC_FILE_DELETEDIR_NOTEMPTY = 0x80030B03,      // 目录不为空
        EZNC_FILE_DELETEDIR_ALREADYOPENED = 0x80030B01, // 目录已打开
        EZNC_FILE_DELETEDIR_NOTDELETE = 0x80030B05,    // 目录未删除
        EZNC_FILE_DELETEDIR_WRITE_PROTECT = 0x80030BB2, // 写保护错误
        EZNC_PCFILE_DELETEDIR_ILLEGALNAME = 0x80030BA7, // PC非法名称错误
        EZNC_PCFILE_DELETEDIR_NODIR = 0x80030BA2,        // PC目录不存在
        EZNC_PCFILE_DELETEDIR_NAMELENGTH = 0x80030BA9, // PC名称长度错误
        EZNC_PCFILE_DELETEDIR_NOTEMPTY = 0x80030B04,  // PC目录不为空
        EZNC_PCFILE_DELETEDIR_ALREADYOPENED = 0x80030B02, // PC目录已打开
        EZNC_PCFILE_DELETEDIR_NOTDELETE = 0x80030B06, // PC目录未删除
    }

}
