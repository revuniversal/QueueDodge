﻿while(1) { sleep -sec 30; Invoke-RestMethod "http://localhost:5001/api/region/us/bracket/2v2?locale=en_us"; echo "done" }