var gulp = require('gulp');
var sass = require('gulp-sass');
var markdown = require('gulp-markdown');
// bundling
var SystemBuilder = require('systemjs-builder');
var argv = require('yargs').argv;
var builder = new SystemBuilder();


gulp.task('sass', function () {
    gulp.src('./wwwroot/app/styles/style.scss')
        .pipe(sass())
        .pipe(gulp.dest(function (f) {
            return f.base;
        }));
});

gulp.task('bundle', function(){
    builder.loadConfig('./system.config.js')
  .then(function(){
	  var outputFile = argv.prod ? 'dist/bundle.min.js' : 'dist/bundle.js';
	  return builder.buildStatic('app', outputFile, {
		  minify: argv.prod,
		  mangle: argv.prod,
		  rollup: argv.prod
	  });
  })
  .then(function(){
	  console.log('bundle built successfully!');
});
});

gulp.task('markdown', function () {
    return gulp.src('**/*.md')
        .pipe(markdown())
        .pipe(gulp.dest(function (f) {
            return f.base;
        }));
});

gulp.task('default', function () {
    gulp.watch('./wwwroot/app/styles/style.scss', ['sass']);
    gulp.watch('**/*.md', ['markdown']);
});