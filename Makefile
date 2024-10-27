docker.build:
	docker build . -t geocalc

docker.run:
	docker run -d \
		-p 5050:5000 \
		--restart=always \
		--env-file=.env \
		geocalc 

watch:
	@set -o allexport; source .env; set +o allexport; dotnet watch
