using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LBSJourneyWebService.Models.Entity;

/// <summary>
/// 景點 Entity
/// </summary>
[Table(name:"Places")]
public class Places
{
    /// <summary>
    /// 景點 ID
    /// </summary>
    [Key]
    [Column(name:"PlaceId")]
    public String PlaceId { get; set; }
    /// <summary>
    /// 景點名稱
    /// </summary>
    [Column(name:"Name")]
    [MaxLength(100)]
    public String Name { get; set; }
    /// <summary>
    /// 景點地址
    /// </summary>
    [Column(name:"Address")]
    [MaxLength(100)]
    public String Address { get; set; }
    /// <summary>
    /// 景點描述
    /// </summary>
    [Column(name:"Description")]
    public String Description { get; set; }
    /// <summary>
    /// 景點圖片 URL
    /// </summary>
    [Column(name:"PictureUrl")]
    [MaxLength(255)]
    public String PictureUrl { get; set; }
    /// <summary>
    /// 景點評分
    /// </summary>
    [Column(name:"Score")]
    public Int32 Score { get; set; }
    /// <summary>
    /// 景點緯度
    /// </summary>
    [Column(name:"Latitude")]
    public Decimal Latitude { get; set; }
    /// <summary>
    /// 景點經度
    /// </summary>
    [Column(name:"Longitude")]
    public Decimal Longitude { get; set; }
}