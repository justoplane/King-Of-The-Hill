const std = @import("std");
const process = std.process;
const debug = std.debug;

pub fn main() !void {
    var args_no_alloc_iterator = process.args();
    var port: u32 = 420;

    while (args_no_alloc_iterator.next()) |arg| {
        debug.print("arg: {s}\n", .{arg});
        if (std.mem.eql(u8, arg, "--port") or std.mem.eql(u8, arg, "-P")) {
            const port_str_opt = args_no_alloc_iterator.next();
            if (port_str_opt) |port_str| {
                // Parse the string into an integer
                port = try std.fmt.parseInt(u32, port_str, 10);
            } else {
                debug.print("Error: --port specified but no value provided.\n", .{});
                return;
            }
        }
    }

    debug.print("Using port: {}\n", .{port});
}
