# ภาพรวมโปรเจค (Overview)

โปรเจคนี้เป็นแอปพลิเคชัน .NET (ASP.NET Core) ขนาดเล็ก ประกอบด้วยโค้ดเว็บและชุดทดสอบอัตโนมัติ โครงสร้างของโปรเจคใช้ `Web.csproj` เป็นโปรเจคหลักสำหรับเว็บ และมีโฟลเดอร์ `Tests` สำหรับ unit tests (ไฟล์ `CalculatorTests.cs`) 

โปรเจคถูกออกแบบให้เรียบง่ายเพื่อสาธิตการทำงานของแอปเว็บ, การทดสอบด้วย `dotnet test` และการ build/publish ด้วย `dotnet build` / `dotnet publish`.

**แพลตฟอร์มที่ต้องการ**: .NET 8 (หรือ .NET SDK ที่เข้ากันได้)

**โครงสร้างหลักของ repository**

- **`Web.csproj`**: ไฟล์โปรเจคสำหรับแอปเว็บ
- **`Web.sln`**: Solution file (ถ้ามี) เพื่อรัน build/test ทั้ง solution
- **`Pages/Index.cshtml`**, **`Pages/Index.cshtml.cs`**: หน้า Razor page ตัวอย่าง
- **`Calculator.cs`, `Options.cs`, `Program.cs`, `Web.csproj`**: โค้ดหลักของแอป
- **`Tests/Calculator.Tests.csproj`** และ **`Tests/CalculatorTests.cs`**: โปรเจคและไฟล์ทดสอบ
- **`bin/`**, **`obj/`**: โฟลเดอร์ที่ถูกสร้างเมื่อ build/publish

**หมายเหตุ**: อย่าแปลคำศัพท์เทคนิค เช่น `src`, `bin`, `cshtml` — ไว้เหมือนเดิมในเอกสารนี้

**รายละเอียดไฟล์และโฟลเดอร์สำคัญ**

- **`Program.cs`**: จุดเริ่มต้นของแอป ขึ้นค่า host, routing และ service ต่าง ๆ
- **`Calculator.cs`**: ตัวอย่างคลาสที่น่าจะมี logic เพื่อใช้ใน unit tests
- **`Pages/Index.cshtml`**: หน้า web UI แบบ Razor
- **`Tests/Calculator.Tests.csproj`**: โปรเจคทดสอบ unit test เช่น xUnit หรือ NUnit (ตรวจสอบไฟล์เพื่อดู framework ที่ใช้งาน)

**คำสั่งที่ใช้บ่อย (Terminal)**

-- ติดตั้ง/ตรวจสอบ SDK (แนะนำตรวจสอบเวอร์ชันก่อน)
```
dotnet --version
```

- Restore dependencies (ถ้าจำเป็น)
```
dotnet restore
```

- Build (ระดับ solution หรือ project)
```
dotnet build
```
หรือเฉพาะโปรเจคเว็บ
```
dotnet build Web.csproj
```

- Run unit tests (ทั้ง solution หรือเฉพาะโฟลเดอร์ Tests)
```
dotnet test
```
หรือเจาะจงโปรเจคทดสอบ
```
dotnet test Tests/Calculator.Tests.csproj
```

- รันเว็บแอป (พัฒนา / รันแบบ dev)
```
dotnet run --project Web.csproj
```
โดยปกติคำสั่งด้านบนจะแสดง URL ที่เว็บรันอยู่ (เช่น `http://localhost:5000` หรือ `https://localhost:5001`) ให้เปิด URL นั้นด้วยเบราว์เซอร์

- กำหนดพอร์ตแบบชั่วคราว แล้วรัน (ตัวอย่างให้ทดสอบด้วยคำสั่ง `curl`)
```
ASPNETCORE_URLS="http://localhost:5000" dotnet run --project Web.csproj
# แล้วทดสอบด้วย
curl http://localhost:5000
```

- Publish เพื่อเอาไป deploy (release)
```
dotnet publish Web.csproj -c Release -o ./publish
```
ไฟล์ที่ได้จะอยู่ใน `./publish` หรือใน `bin/Release/net8.0/publish`

**รันทดสอบแอพ (automated / manual smoke test)**

1. รันเว็บแอป (ตัวอย่างพอร์ต 5000):
```
ASPNETCORE_URLS="http://localhost:5000" dotnet run --project Web.csproj
```
2. ในเทอร์มินัลใหม่ ทำ smoke test แบบง่ายๆ ด้วย `curl`:
```
curl -i http://localhost:5000/
```
3. ถ้ามี endpoint เฉพาะสำหรับการทดสอบ เช่น `/health` ให้เรียก endpoint นั้นเพื่อเช็กสถานะ

**การรัน CI / Local checks (ข้อเสนอแนะ)**

- ลำดับคำสั่งที่แนะนำก่อน push/PR:
```
dotnet restore
dotnet build --no-restore
dotnet test --no-build
```

**ข้อแนะนำเพิ่มเติม**

- หากต้องการรันเว็บในเบื้องหลัง ใช้ `dotnet publish` แล้วรัน binary ที่ `./publish` หรือใช้ systemd/docker ตามต้องการ
- หากต้องการ debug ให้ใช้ IDE เช่น Visual Studio / VS Code (+ C# extension) เพื่อรันและ debug แบบง่าย

---

หากต้องการผมสามารถ:
- เพิ่มตัวอย่าง integration test แบบอัตโนมัติที่เรียก HTTP endpoint
- เพิ่ม `README` ภาษาอังกฤษคู่กับภาษาไทย
- สร้าง `launch.json` และ `tasks.json` สำหรับ VS Code เพื่อให้รัน/ดีบักได้ง่ายขึ้น

บอกผมว่าต้องการอะไรต่อไปครับ

---

Copyright © 2025 Keerati. All rights reserved.
