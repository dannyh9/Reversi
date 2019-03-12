const gulp = require('gulp');
var sass = require('gulp-sass');
sass.compiler = require('node-sass');
var uglify = require('gulp-uglify');
const {src, dest} = require('gulp');


gulp.task('read-js-files', function () {
    return src('./js/**/*.js').pipe(dest('dist'));
});

const clean_css = require('gulp-clean-css');
const auto_prefixer = require('gulp-autoprefixer');
const concat = require('gulp-concat');

gulp.task('css', function () {
    return src('./wwwroot/css/**/*.css')
        .pipe(clean_css({ compatibility: 'ie9' }))
        .pipe(auto_prefixer('last 2 version', 'safari 5', 'ie 9'))
        .pipe(concat('style.min.css'))
        .pipe(gulp.dest('./wwwroot/dist'));
});

gulp.task('scss', function () {
    return src('./wwwroot/sass/**/*.scss')
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('./wwwroot/dist'));
});


gulp.task('js', function () {
    //.pipe(uglify())
    return src(['./wwwroot/js/**/*.js'], ['./wwwroot/js/*.js'])
        .pipe(concat('app.js'))
        .pipe(gulp.dest('./wwwroot/dist'));
});

gulp.task('sass:watch', function () {
    gulp.watch('./wwwroot/sass/**/*.scss', ['sass']);
});