﻿while(1) { sleep -sec 30; Invoke-RestMethod "http://localhost:5001/api/leaderboard?bracket=2v2&region=us&locale=en_us"; echo "done" }