# 疫苗預約平台 專題 手機端

本專題的功能主要是圍繞在疫苗預約以及其他跟疫苗相關的內容

## API Reference

#### 獲得疫情新聞

```http
  GET /api/news
```

#### 獲得特定疫情新聞

```http
  GET /api/news/${id}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `id`      | `string` | **Required**. Id of item to fetch |

#### 簡易身分驗證

```http
  POST /api/easycheck
```

```http
  PUT /api/easycheck
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ROCId`      | `string` | **Required**. Your personal ID |
| `Year`      | `string` | **Required**. Your born(year) |

#### 疫苗施打地點查詢

```http
  POST /api/vachosp
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `city`      | `string` |  city of place |
| `dist`      | `string` |  dist of place |
| `vaccine`      | `string` |  vaccine type |

#### 查詢疫苗預約

```http
  POST /api/reserve
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ROCId`      | `string` | **Required**. Your personal ID |

#### 預約疫苗

```http
  PUT /api/reserve
```
| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ROCId`      | `string` | **Required**. Your personal ID |
| `Name`      | `string` | **Required**. Your Name |
| `Phone`      | `string` | **Required**. Your Phone |
| `VaccineType`      | `string` | **Required**. Vaccine Type |
| `HospName`      | `string` | **Required**. Hospital Name |

#### 取消預約

```http
  DELETE /api/reserve
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `ROCId`      | `string` | **Required**. Your personal ID |vaccine type |

## Video Demo

https://youtu.be/Y1CdWKmWl4A
