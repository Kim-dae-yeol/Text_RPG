# Text_RPG
스파르타 코딩클럽 내일배움캠프 C# 기본주차 개인과제입니다.
<br>


## ⏰ 개발기한 
 - 2023.08.21 ~  2023.08.23

## ⚙️ 개발환경
  - C# 11.0
  - .Net 7.0
  - MVVM 패턴 적용
## 📌 주요기능 
 - 상태창<br>
    상태창에서는 캐릭터의 장비와 현재 스탯을 확인할 수 있습니다. 방향키로 움직이면 장비창에서 이동이 가능합니다. 또한 Enter 를 이용해서 장비 변경이 가능한데 이때 뜨는 인벤토리 창에서는 선택한 장비칸의 아이템만 선택할 수 있도록 처리했습니다.
 <br>
   <img width="640" alt="status1" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/6aa98236-22e3-4644-ad36-8338ba89df5d">
   <br>
   <img width="640" alt="status2" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/b8b1dbb2-e271-44af-893a-8217ebab7601">
   <br>

   - 인벤토리<br>
 인벤토리 페이지에서는 아이템을 장착하거나 인벤토리의 아이템을 버리거나 인벤토리를 정렬할 수 있습니다. 인벤토리의 최대칸은 15개입니다.<br>
    <img width="640" alt="StatusToInventory2" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/e0c9708b-afe8-41aa-8732-1969bb32ea4a"><br>
    <img width="640" alt="statusToInventory1" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/53bfa205-1615-45e8-81c7-4960d5dc8971"><br>


- 마을 <br>
마을에서는 상점을 찾아가거나 강화 (아직 구현 안됨 )와 던전으로 입장 할 수 있습니다. 또한 현재 상태와 인벤토리를 확인할 수 있습니다.<br>
<img width="640" alt="Town4" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/55719fff-1846-43f1-816d-7402efff54cc">
<img width="640" alt="Town3" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/251697df-b9c7-4d7a-a962-1b11903aa0c5">
<img width="640" alt="Town2" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/904fb98b-9fad-4dbc-a8c6-bb8c9c34b147">
<img width="640" alt="Town" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/874a3861-e1be-4968-92c2-2f4259955ec2">
<br>


 - 던전<br>
 던전 기능은 시간의 부족함으로 비교적 부실하게 구현되었습니다. 레벨디자인및 클리어 조건도 부실하지만 게임내부에서 돈을 벌 수 있습니다 <br>
<img width="640" alt="Dungeon" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/6dde93c3-17d3-4928-ab13-c0f9c1432d27">
  
 - 저장<br>
파일 시스템을 이용해서 저장합니다. 프로젝트 내부 csv로 저장하며 코드에서 txt 를 읽어서 메모리상에 올립니다. <br>
<img width="142" alt="FileSystem" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/19569930-b071-4997-a144-5e85aa021e0b">
<img width="640" alt="CsvFormat" src="https://github.com/Kim-dae-yeol/Text_RPG/assets/115692722/5a22ee3c-9c54-4e49-8708-f6aaeb9f1157"><br>
