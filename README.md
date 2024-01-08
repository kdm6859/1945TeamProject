## 프로젝트 소개

- 2D 슈팅 게임 구현
- 적 유닛, 총알 등 오브젝트 풀링 방식으로 관리

## 시연 영상

https://youtu.be/9RixwHjd1k8

## 목차

1. 플레이어 움직임 및 공격
2. 적 스폰 시스템 구현
3. 보스 UI 및 패턴 구현

### 1. 플레이어 움직임 및 공격

- 상하좌우 움직임 및 총알 발사 구현
- 총알은 오브젝트 풀링 방식으로 관리
- 아이템 획득 시 공격 강화

![Untitled (1)](https://github.com/kdm6859/1945TeamProject/assets/64892955/2243c15a-3c71-4508-b626-24df1bef6d6d)

### 2. 적 스폰 시스템 구현

- 적 유닛 랜덤한 위치에 스폰
- 시간 경과에 따라 약한 유닛 → 강한 유닛 생성
- 적 유닛 스폰 후 플레이어 공격, 일정 시간 후 맵 밖으로 이동
- hp가 0이 되면 폭발이펙트 생성
- 적 유닛 오브젝트 풀링 방식으로 관리

![Untitled (2)](https://github.com/kdm6859/1945TeamProject/assets/64892955/9910fc90-5fff-4486-a824-af0cc3935000)

### 3. 보스 UI 및 패턴 구현

- 보스 등장 시 보스UI 등장
- 보스 공격 패턴 구현, 시간 경과에 따라 순서대로 패턴 발생
- 보스 공격 경고 시스템 구현
- 보스 처치 시 2페이즈 진입

![Untitled (3)](https://github.com/kdm6859/1945TeamProject/assets/64892955/75c3be79-83f4-450b-bae5-c77dda864ae7) ![Untitled (4)](https://github.com/kdm6859/1945TeamProject/assets/64892955/413f10c9-97da-4caf-a4a6-39e70f98b092)

![Untitled (5)](https://github.com/kdm6859/1945TeamProject/assets/64892955/047665a7-a7fb-4c4e-9304-3552d3324354)

![Untitled (6)](https://github.com/kdm6859/1945TeamProject/assets/64892955/2bc77143-635e-4e72-bf6d-0b0eb855a8a2) ![Untitled (7)](https://github.com/kdm6859/1945TeamProject/assets/64892955/046f6639-eb6d-4232-b8e7-6f147b71dc37)
